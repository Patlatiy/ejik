using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Ejik
{
    public partial class FormSettings : Form
    {
        private Microsoft.Win32.RegistryKey rkApp = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        public static FormSettings curForm = null;

        public FormSettings()
        {
            InitializeComponent();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            FillRules();

            if (rkApp.GetValue("Ejik") == null)
            {
                chkStartup.Checked = false;
            }
            else
            {
                chkStartup.Checked = true;
            }
            curForm = this;
        }

        private void FillRules()
        {
            comboRules.Items.Clear();
            foreach (Watcher watcher in Watcher.AllOfThem)
            {
                ComboWatcherItem tmpItem = new ComboWatcherItem(watcher.WatchPath, watcher);
                comboRules.Items.Add(tmpItem);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            btnApply_Click(sender, e);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboRules_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtWatchPath.Text = ((ComboWatcherItem)comboRules.SelectedItem).Watcher.WatchPath;
            txtMovePath.Text = ((ComboWatcherItem)comboRules.SelectedItem).Watcher.MovePath;
            txtFilter.Text = ((ComboWatcherItem)comboRules.SelectedItem).Watcher.Filter;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (comboRules.SelectedIndex != -1)
            {
                if (!Directory.Exists(txtWatchPath.Text))
                {
                    MessageBox.Show("Watch directory " + txtWatchPath.Text + " doesn't exist.", "Something is wrong...", MessageBoxButtons.OK);
                }
                else if (!Directory.Exists(txtMovePath.Text))
                {
                    MessageBox.Show("Move directory " + txtMovePath.Text + " doesn't exist.", "Something is wrong...", MessageBoxButtons.OK);
                }
                else if (txtFilter.Text == "")
                {
                    MessageBox.Show("The filter is empty. Please enter filter. You can use \"*\" and \"?\" symbols. Separate filters by \",\" or \"|\".");
                }
                else
                {
                    ((ComboWatcherItem)comboRules.SelectedItem).Watcher.WatchPath = txtWatchPath.Text;
                    ((ComboWatcherItem)comboRules.SelectedItem).Watcher.MovePath = txtMovePath.Text;
                    ((ComboWatcherItem)comboRules.SelectedItem).Watcher.Filter = txtFilter.Text;
                    Watcher.SaveToSettings();

                    //black magic here:
                    ((ComboWatcherItem)comboRules.SelectedItem).Text = txtWatchPath.Text;
                    typeof(ComboBox).InvokeMember("RefreshItems", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.InvokeMethod, null, comboRules, new object[] { });
                }
            }

            if (chkStartup.Checked)
            {
                rkApp.SetValue("Ejik", Application.ExecutablePath.ToString());
            }
            else
            {
                rkApp.DeleteValue("Ejik", false);
            }

            Form1.LastInstance.SetTooltip();
        }

        private void btnAddRule_Click(object sender, EventArgs e)
        {
            Watcher newWatcher = new Watcher(Application.StartupPath, Path.Combine(Application.StartupPath, "Moved by Ejik"), "*.jpg|*.jpeg|*.gif|*.png|*.bmp");
            ComboWatcherItem newItem = new ComboWatcherItem(newWatcher.WatchPath, newWatcher);
            comboRules.Items.Add(newItem);
            comboRules.SelectedItem = newItem;
        }

        private void btnRemoveRule_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete this rule?", "Rule deletion", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                ((ComboWatcherItem)comboRules.SelectedItem).Watcher.Dispose();
                Watcher.SaveToSettings();
                comboRules.Items.Remove(comboRules.SelectedItem);
                txtFilter.Text = string.Empty;
                txtMovePath.Text = string.Empty;
                txtWatchPath.Text = string.Empty;
            }
        }

        private void btnSelectWatchPath_Click(object sender, EventArgs e)
        {
            txtWatchPath.Text = SelectDirectory(txtWatchPath.Text);
        }

        private void btnSelectMovePath_Click(object sender, EventArgs e)
        {
            txtMovePath.Text = SelectDirectory(txtMovePath.Text);
        }

        private string SelectDirectory(string defaultPath)
        {
            fbd.RootFolder = Environment.SpecialFolder.MyComputer;
            if (Directory.Exists(defaultPath))
            {
                fbd.SelectedPath = defaultPath;
            }
            else
            {
                string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string pathDownload = Path.Combine(pathUser, "Downloads");
                if (Directory.Exists(pathDownload))
                    fbd.SelectedPath = pathDownload;
                else
                    fbd.SelectedPath = pathUser;
            }
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                return fbd.SelectedPath;
            }
            return defaultPath;
        }

        private void FormSettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            curForm = null;
        }
    }

    public class ComboWatcherItem
    {
        private string _text;
        private Watcher _watcher;

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public Watcher Watcher
        {
            get { return _watcher; }
            set { _watcher = value; }
        }

        public ComboWatcherItem(string text, Watcher watcher)
        {
            _text = text;
            _watcher = watcher;
        }

        public override string ToString()
        {
            return _text;
        }
    }
}
