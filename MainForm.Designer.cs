namespace DynamicBrightnessApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TrackBar trackBarBrightness;
        private System.Windows.Forms.Label labelStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.trackBarBrightness = new System.Windows.Forms.TrackBar();
            this.labelStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBarBrightness
            // 
            this.trackBarBrightness.Location = new System.Drawing.Point(12, 45);
            this.trackBarBrightness.Maximum = 100;
            this.trackBarBrightness.Name = "trackBarBrightness";
            this.trackBarBrightness.Size = new System.Drawing.Size(360, 45);
            this.trackBarBrightness.TabIndex = 0;
            this.trackBarBrightness.TickFrequency = 10;
            this.trackBarBrightness.Enabled = false; // read-only slider showing current brightness
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(12, 20);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(160, 17);
            this.labelStatus.TabIndex = 1;
            this.labelStatus.Text = "Starting brightness monitor...";
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(384, 100);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.trackBarBrightness);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Dynamic Brightness App";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
