namespace DynamicBrightnessApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TrackBar trackBarBrightness;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.ComboBox comboBoxProfiles;
        private System.Windows.Forms.Button btnAddProfile;
        private System.Windows.Forms.Button btnRules;
        private System.Windows.Forms.CheckBox checkBoxGlobalToggle;
        private System.Windows.Forms.NumericUpDown numericDelay;
        private System.Windows.Forms.Label labelDelay;
        private System.Windows.Forms.Panel panelMain;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelMain = new System.Windows.Forms.Panel();
            this.labelStatus = new System.Windows.Forms.Label();
            this.trackBarBrightness = new System.Windows.Forms.TrackBar();
            this.comboBoxProfiles = new System.Windows.Forms.ComboBox();
            this.btnAddProfile = new System.Windows.Forms.Button();
            this.btnRules = new System.Windows.Forms.Button();
            this.checkBoxGlobalToggle = new System.Windows.Forms.CheckBox();
            this.numericDelay = new System.Windows.Forms.NumericUpDown();
            this.labelDelay = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDelay)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.White;
            this.panelMain.Location = new System.Drawing.Point(20, 20);
            this.panelMain.Size = new System.Drawing.Size(400, 220);
            this.panelMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.panelMain.Padding = new System.Windows.Forms.Padding(20);
            this.panelMain.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelMain.TabIndex = 0;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.labelStatus.ForeColor = System.Drawing.Color.FromArgb(40, 40, 60);
            this.labelStatus.Location = new System.Drawing.Point(20, 20);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(300, 30);
            this.labelStatus.TabIndex = 1;
            this.labelStatus.Text = "Dynamic Brightness ✨";
            // 
            // trackBarBrightness
            // 
            this.trackBarBrightness.Location = new System.Drawing.Point(20, 60);
            this.trackBarBrightness.Maximum = 100;
            this.trackBarBrightness.Name = "trackBarBrightness";
            this.trackBarBrightness.Size = new System.Drawing.Size(300, 45);
            this.trackBarBrightness.TabIndex = 2;
            this.trackBarBrightness.TickFrequency = 10;
            this.trackBarBrightness.BackColor = System.Drawing.Color.White;
            this.trackBarBrightness.Enabled = true;
            // 
            // comboBoxProfiles
            // 
            this.comboBoxProfiles.Location = new System.Drawing.Point(20, 110);
            this.comboBoxProfiles.Size = new System.Drawing.Size(180, 30);
            this.comboBoxProfiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProfiles.Font = new System.Drawing.Font("Segoe UI", 11F);
            // 
            // btnAddProfile
            // 
            this.btnAddProfile.Location = new System.Drawing.Point(210, 110);
            this.btnAddProfile.Size = new System.Drawing.Size(110, 30);
            this.btnAddProfile.Text = "Add Profile";
            this.btnAddProfile.Font = new System.Drawing.Font("Segoe UI", 10F);
            // 
            // btnRules
            // 
            this.btnRules.Location = new System.Drawing.Point(330, 110);
            this.btnRules.Size = new System.Drawing.Size(50, 30);
            this.btnRules.Text = "Rules";
            this.btnRules.Font = new System.Drawing.Font("Segoe UI", 10F);
            // 
            // checkBoxGlobalToggle
            // 
            this.checkBoxGlobalToggle.Location = new System.Drawing.Point(20, 150);
            this.checkBoxGlobalToggle.Size = new System.Drawing.Size(180, 30);
            this.checkBoxGlobalToggle.Text = "Enable Dynamic Brightness";
            this.checkBoxGlobalToggle.Checked = true;
            this.checkBoxGlobalToggle.Font = new System.Drawing.Font("Segoe UI", 10F);
            // 
            // labelDelay
            // 
            this.labelDelay.Location = new System.Drawing.Point(210, 150);
            this.labelDelay.Size = new System.Drawing.Size(60, 30);
            this.labelDelay.Text = "Delay:";
            this.labelDelay.Font = new System.Drawing.Font("Segoe UI", 10F);
            // 
            // numericDelay
            // 
            this.numericDelay.Location = new System.Drawing.Point(270, 150);
            this.numericDelay.Size = new System.Drawing.Size(60, 30);
            this.numericDelay.Minimum = 0;
            this.numericDelay.Maximum = 5000;
            this.numericDelay.Value = 500;
            this.numericDelay.Increment = 100;
            this.numericDelay.Font = new System.Drawing.Font("Segoe UI", 10F);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(440, 260);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Dynamic Brightness";
            this.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            // Add controls to panel
            this.panelMain.Controls.Add(this.labelStatus);
            this.panelMain.Controls.Add(this.trackBarBrightness);
            this.panelMain.Controls.Add(this.comboBoxProfiles);
            this.panelMain.Controls.Add(this.btnAddProfile);
            this.panelMain.Controls.Add(this.btnRules);
            this.panelMain.Controls.Add(this.checkBoxGlobalToggle);
            this.panelMain.Controls.Add(this.labelDelay);
            this.panelMain.Controls.Add(this.numericDelay);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDelay)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
