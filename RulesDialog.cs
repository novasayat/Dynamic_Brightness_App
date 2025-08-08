using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DynamicBrightnessApp
{
    public partial class RulesDialog : Form
    {
        private List<Rule> rules;
        private IEnumerable<string> profiles;
        private ListView listViewRules;
        private Button btnAdd, btnEdit, btnDelete, btnOK;

        public RulesDialog(List<Rule> rules, IEnumerable<string> profiles)
        {
            this.rules = new List<Rule>(rules);
            this.profiles = profiles;
            InitializeComponent();
            PopulateRules();
        }

        private void InitializeComponent()
        {
            this.Text = "Manage Rules";
            this.Size = new System.Drawing.Size(500, 350);
            listViewRules = new ListView { View = View.Details, FullRowSelect = true, Location = new System.Drawing.Point(10, 10), Size = new System.Drawing.Size(460, 220) };
            listViewRules.Columns.Add("App Pattern", 150);
            listViewRules.Columns.Add("Title Pattern", 150);
            listViewRules.Columns.Add("Profile", 150);

            btnAdd = new Button { Text = "Add", Location = new System.Drawing.Point(10, 240), Size = new System.Drawing.Size(100, 30) };
            btnEdit = new Button { Text = "Edit", Location = new System.Drawing.Point(120, 240), Size = new System.Drawing.Size(100, 30) };
            btnDelete = new Button { Text = "Delete", Location = new System.Drawing.Point(230, 240), Size = new System.Drawing.Size(100, 30) };
            btnOK = new Button { Text = "OK", Location = new System.Drawing.Point(370, 240), Size = new System.Drawing.Size(100, 30) };

            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnOK.Click += (s, e) => { this.DialogResult = DialogResult.OK; this.Close(); };

            this.Controls.Add(listViewRules);
            this.Controls.Add(btnAdd);
            this.Controls.Add(btnEdit);
            this.Controls.Add(btnDelete);
            this.Controls.Add(btnOK);
        }

        private void PopulateRules()
        {
            listViewRules.Items.Clear();
            foreach (var rule in rules)
            {
                var item = new ListViewItem(new[] { rule.AppPattern, rule.TitlePattern, rule.Profile });
                listViewRules.Items.Add(item);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var dlg = new RuleEditDialog(profiles);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                rules.Add(dlg.GetRule());
                PopulateRules();
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (listViewRules.SelectedIndices.Count > 0)
            {
                int idx = listViewRules.SelectedIndices[0];
                var dlg = new RuleEditDialog(profiles, rules[idx]);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    rules[idx] = dlg.GetRule();
                    PopulateRules();
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (listViewRules.SelectedIndices.Count > 0)
            {
                int idx = listViewRules.SelectedIndices[0];
                rules.RemoveAt(idx);
                PopulateRules();
            }
        }

        public List<Rule> GetRules() => rules;
    }
}