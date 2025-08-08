using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Management;
using System.IO;
using Newtonsoft.Json;

namespace DynamicBrightnessApp
{
    public partial class MainForm : Form
    {
        private Thread monitorThread;
        private bool monitoring = true;
        private Dictionary<string, int> profiles = new Dictionary<string, int>();
        private List<Rule> rules = new List<Rule>();
        private int defaultBrightness = 50;
        private int delayMs = 500;
        private string settingsPath = "settings.json";

        // WinAPI to get active window
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        public MainForm()
        {
            InitializeComponent();
            LoadSettings();
            PopulateProfiles();
            comboBoxProfiles.SelectedIndexChanged += ComboBoxProfiles_SelectedIndexChanged;
            trackBarBrightness.Scroll += TrackBarBrightness_Scroll;
            btnAddProfile.Click += BtnAddProfile_Click;
            btnRules.Click += BtnRules_Click;
            checkBoxGlobalToggle.CheckedChanged += CheckBoxGlobalToggle_CheckedChanged;
            numericDelay.ValueChanged += NumericDelay_ValueChanged;
            StartMonitoring();
        }

        private void PopulateProfiles()
        {
            comboBoxProfiles.Items.Clear();
            foreach (var kv in profiles)
                comboBoxProfiles.Items.Add($"{kv.Key} ({kv.Value}%)");
            if (comboBoxProfiles.Items.Count > 0)
                comboBoxProfiles.SelectedIndex = 0;
        }

        private void ComboBoxProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxProfiles.SelectedIndex >= 0)
            {
                var profileName = comboBoxProfiles.SelectedItem.ToString().Split('(')[0].Trim();
                if (profiles.ContainsKey(profileName))
                {
                    trackBarBrightness.Value = profiles[profileName];
                    labelStatus.Text = $"Profile: {profileName} ({profiles[profileName]}%)";
                }
            }
        }

        private void TrackBarBrightness_Scroll(object sender, EventArgs e)
        {
            labelStatus.Text = $"Manual Brightness: {trackBarBrightness.Value}%";
            SetBrightness((byte)trackBarBrightness.Value);
        }

        private void BtnAddProfile_Click(object sender, EventArgs e)
        {
            string name = Microsoft.VisualBasic.Interaction.InputBox("Profile name:", "Add Profile", "New Profile");
            if (!string.IsNullOrWhiteSpace(name))
            {
                profiles[name] = trackBarBrightness.Value;
                SaveSettings();
                PopulateProfiles();
                comboBoxProfiles.SelectedItem = $"{name} ({trackBarBrightness.Value}%)";
            }
        }

        private void BtnRules_Click(object sender, EventArgs e)
        {
            var dlg = new RulesDialog(rules, profiles.Keys);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                rules = dlg.GetRules();
                SaveSettings();
            }
        }

        private void CheckBoxGlobalToggle_CheckedChanged(object sender, EventArgs e)
        {
            monitoring = checkBoxGlobalToggle.Checked;
            labelStatus.Text = monitoring ? "Dynamic Brightness Enabled" : "Dynamic Brightness Disabled";
        }

        private void NumericDelay_ValueChanged(object sender, EventArgs e)
        {
            delayMs = (int)numericDelay.Value;
            SaveSettings();
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
                        if (!checkBoxGlobalToggle.Checked)
                        {
                            Thread.Sleep(500);
                            continue;
                        }

                        IntPtr hwnd = GetForegroundWindow();
                        if (hwnd != IntPtr.Zero)
                        {
                            StringBuilder sb = new StringBuilder(256);
                            GetWindowText(hwnd, sb, sb.Capacity);
                            string windowTitle = sb.ToString();
                            string processName = GetProcessNameFromWindow(hwnd);
                            string appKey = processName ?? "default";

                            Rule matchedRule = null;
                            foreach (var rule in rules)
                            {
                                if (rule.Match(processName, windowTitle))
                                {
                                    matchedRule = rule;
                                    break;
                                }
                            }

                            int brightness = matchedRule != null && profiles.ContainsKey(matchedRule.Profile)
                                ? profiles[matchedRule.Profile]
                                : defaultBrightness;

                            if (appKey != lastApp)
                            {
                                lastApp = appKey;
                                Thread.Sleep(delayMs);
                                SetBrightness((byte)brightness);
                                // Always update status label
                                this.Invoke((Action)(() =>
                                {
                                    labelStatus.Text = $"Active: {appKey} | {windowTitle} | Brightness: {brightness}%";
                                    trackBarBrightness.Value = brightness;
                                }));
                            }
                            else
                            {
                                // Still update status label if nothing changed
                                this.Invoke((Action)(() =>
                                {
                                    labelStatus.Text = $"Active: {appKey} | {windowTitle} | Brightness: {brightness}%";
                                }));
                            }
                        }
                    }
                    catch
                    {
                        // Optionally log error
                    }
                    Thread.Sleep(500);
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
                        mObj.InvokeMethod("WmiSetBrightness", new object[] { UInt32.MaxValue, targetBrightness });
                        break;
                    }
                }
            }
            catch { }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            monitoring = false;
            if (monitorThread != null && monitorThread.IsAlive)
                monitorThread.Join(1000);
            base.OnFormClosing(e);
        }

        private void LoadSettings()
        {
            if (File.Exists(settingsPath))
            {
                var json = File.ReadAllText(settingsPath);
                var settings = JsonConvert.DeserializeObject<AppSettings>(json);
                profiles = settings.Profiles ?? new Dictionary<string, int>();
                rules = settings.Rules ?? new List<Rule>();
                defaultBrightness = settings.DefaultBrightness;
                delayMs = settings.DelayMs;
                numericDelay.Value = delayMs;
            }
            else
            {
                profiles = new Dictionary<string, int> { { "Default", 50 }, { "Low Comfort", 40 }, { "Medium", 60 }, { "High", 80 } };
                rules = new List<Rule>();
                defaultBrightness = 50;
                delayMs = 500;
            }
        }

        private void SaveSettings()
        {
            var settings = new AppSettings
            {
                Profiles = profiles,
                Rules = rules,
                DefaultBrightness = defaultBrightness,
                DelayMs = delayMs
            };
            File.WriteAllText(settingsPath, JsonConvert.SerializeObject(settings, Formatting.Indented));
        }
    }

    public class AppSettings
    {
        public Dictionary<string, int> Profiles { get; set; }
        public List<Rule> Rules { get; set; }
        public int DefaultBrightness { get; set; }
        public int DelayMs { get; set; }
    }

    public class Rule
    {
        public string AppPattern { get; set; }
        public string TitlePattern { get; set; }
        public string Profile { get; set; }

        public bool Match(string processName, string windowTitle)
        {
            bool appMatch = string.IsNullOrWhiteSpace(AppPattern) || WildcardMatch(processName, AppPattern);
            bool titleMatch = string.IsNullOrWhiteSpace(TitlePattern) || WildcardMatch(windowTitle, TitlePattern);
            return appMatch && titleMatch;
        }

        private bool WildcardMatch(string input, string pattern)
        {
            if (string.IsNullOrEmpty(pattern)) return true;
            pattern = "^" + System.Text.RegularExpressions.Regex.Escape(pattern).Replace("\\*", ".*").Replace("\\?", ".") + "$";
            return System.Text.RegularExpressions.Regex.IsMatch(input ?? "", pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }
    }
}
