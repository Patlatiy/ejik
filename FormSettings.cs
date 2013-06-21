using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ejik
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            foreach (Watcher watcher in Watcher.AllOfThem)
            {
                ComboWatcherItem tmpItem = new ComboWatcherItem(watcher.WatchPath, watcher);
                comboRules.Items.Add(tmpItem);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

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
            txtFilter.Text = ((ComboWatcherItem)comboRules.SelectedItem).Watcher.filter;
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
