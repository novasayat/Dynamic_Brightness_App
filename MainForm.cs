using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Management; // Add reference to System.Management

namespace DynamicBrightnessApp
{
    public partial class MainForm : Form
    {
        private Thread monitorThread;
        private bool monitoring = true;
        private Dictionary<string, byte> appBrightnessMap;

        // WinAPI to get active window
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        public MainForm()
        {
            InitializeComponent();

            // Map app process names or window titles to brightness (0-100)
            appBrightnessMap = new Dictionary<string, byte>(StringComparer.OrdinalIgnoreCase)
            {
                { "chrome.exe", 80 },     // Chrome windows set brightness to 80%
                { "notepad.exe", 40 },    // Notepad windows set to 40%
                { "code.exe", 70 },       // VSCode windows to 70%
                { "default", 50 }         // Default brightness when no match
            };

            StartMonitoring();
        }

        private void StartMonitoring()
        {
            monitorThread = new Thread(() =>
            {
                string lastApp = "";
                while (monitoring)
                {
                    try
                    {
                        IntPtr hwnd = GetForegroundWindow();
                        if (hwnd != IntPtr.Zero)
                        {
                            StringBuilder sb = new StringBuilder(256);
                            GetWindowText(hwnd, sb, sb.Capacity);
                            string windowTitle = sb.ToString();

                            // Get process name by hwnd
                            string processName = GetProcessNameFromWindow(hwnd);

                            string appKey = processName ?? "default";

                            if (appKey != lastApp)
                            {
                                lastApp = appKey;
                                byte brightness = appBrightnessMap.ContainsKey(appKey) ? appBrightnessMap[appKey] : appBrightnessMap["default"];
                                SetBrightness(brightness);
                                this.Invoke((Action)(() =>
                                {
                                    labelStatus.Text = $"Active App: {appKey}, Set brightness: {brightness}%";
                                    trackBarBrightness.Value = brightness;
                                }));
                            }
                        }
                    }
                    catch
                    {
                        // ignore errors in monitoring thread
                    }
                    Thread.Sleep(1000);
                }
            });
            monitorThread.IsBackground = true;
            monitorThread.Start();
        }

        private string GetProcessNameFromWindow(IntPtr hwnd)
        {
            try
            {
                uint pid;
                GetWindowThreadProcessId(hwnd, out pid);
                var proc = System.Diagnostics.Process.GetProcessById((int)pid);
                return proc.ProcessName + ".exe";
            }
            catch
            {
                return null;
            }
        }

        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        // Set brightness via WMI
        private void SetBrightness(byte targetBrightness)
        {
            try
            {
                ManagementScope scope = new ManagementScope("root\\WMI");
                SelectQuery query = new SelectQuery("WmiMonitorBrightnessMethods");
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query))
                {
                    foreach (ManagementObject mObj in searcher.Get())
                    {
                        // Timeout in milliseconds = 1 second
                        mObj.InvokeMethod("WmiSetBrightness", new object[] { UInt32.MaxValue, targetBrightness });
                        break; // only do first monitor
                    }
                }
            }
            catch
            {
                // could not set brightness - ignore
            }
        }

        // Cleanup thread on close
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            monitoring = false;
            if (monitorThread != null && monitorThread.IsAlive)
                monitorThread.Join();
            base.OnFormClosing(e);
        }
    }
}
