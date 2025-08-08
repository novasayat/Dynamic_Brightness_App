using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DynamicBrightnessApp
{
    public partial class RuleEditDialog : Form
    {
        private TextBox txtAppPattern, txtTitlePattern;
        private ComboBox comboProfile;
        private Button btnOK, btnCancel;

        public RuleEditDialog(IEnumerable<string> profiles, Rule rule = null)
        {
            InitializeComponent();
            comboProfile.Items.AddRange(new List<string>(profiles).ToArray());
            if (rule != null)
            {
                txtAppPattern.Text = rule.AppPattern;
                txtTitlePattern.Text = rule.TitlePattern;
                comboProfile.SelectedItem = rule.Profile;
            }
            else if (comboProfile.Items.Count > 0)
            {
                comboProfile.SelectedIndex = 0;
            }
        }

        private void InitializeComponent()
        {
            this.Text = "Edit Rule";
            this.Size = new System.Drawing.Size(400, 220);
            var lblApp = new Label { Text = "App Pattern:", Location = new System.Drawing.Point(20, 20), Size = new System.Drawing.Size(100, 25) };
            txtAppPattern = new TextBox { Location = new System.Drawing.Point(130, 20), Size = new System.Drawing.Size(220, 25) };
            var lblTitle = new Label { Text = "Title Pattern:", Location = new System.Drawing.Point(20, 60), Size = new System.Drawing.Size(100, 25) };
            txtTitlePattern = new TextBox { Location = new System.Drawing.Point(130, 60), Size = new System.Drawing.Size(220, 25) };
            var lblProfile = new Label { Text = "Profile:", Location = new System.Drawing.Point(20, 100), Size = new System.Drawing.Size(100, 25) };
            comboProfile = new ComboBox { Location = new System.Drawing.Point(130, 100), Size = new System.Drawing.Size(220, 25), DropDownStyle = ComboBoxStyle.DropDownList };

            btnOK = new Button { Text = "OK", Location = new System.Drawing.Point(130, 150), Size = new System.Drawing.Size(80, 30) };
            btnCancel = new Button { Text = "Cancel", Location = new System.Drawing.Point(220, 150), Size = new System.Drawing.Size(80, 30) };

            btnOK.Click += (s, e) => { this.DialogResult = DialogResult.OK; this.Close(); };
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

            this.Controls.Add(lblApp);
            this.Controls.Add(txtAppPattern);
            this.Controls.Add(lblTitle);
            this.Controls.Add(txtTitlePattern);
            this.Controls.Add(lblProfile);
            this.Controls.Add(comboProfile);
            this.Controls.Add(btnOK);
            this.Controls.Add(btnCancel);
        }

        public Rule GetRule()
        {
            return new Rule
            {
                AppPattern = txtAppPattern.Text.Trim(),
                TitlePattern = txtTitlePattern.Text.Trim(),
                Profile = comboProfile.SelectedItem?.ToString() ?? ""
            };
        }
    }
}