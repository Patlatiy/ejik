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
using System.Security.Permissions;
using System.Configuration;

namespace Ejik
{
    public partial class Form1 : Form
    {
        public static List<String> queFile = new List<string>();
        public static List<String> queDir = new List<string>();
        public static EjikSettings MySettings = new EjikSettings();
        public static Form1 LastInstance;

       

        public Form1()
        {
            InitializeComponent();
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private void Form1_Load(object sender, EventArgs e)
        {
            LastInstance = this;
            loadSettings();
            moveTimer.Start();
            Watcher.LoadFromSettings();
            SetTooltip();

            Timer startupTimer = new Timer();
            startupTimer.Interval = 100;
            startupTimer.Tick += StartupTimer_Tick;
            startupTimer.Start();

            this.txtMoved.Text = Timestamp() + "Ejik welcome you!";
        }

        private void moveTimer_Tick(object sender, EventArgs e)
        {
            txtPending.Clear();
            if (queFile.Count != 0)
            {
                this.lblPending.Text = "Files pending: " + queFile.Count.ToString();
                for (int i = 0; i < queFile.Count; i++)
                {
                    txtPending.Text = queFile[i].Substring(queFile[i].LastIndexOf("\\") + 1) + Environment.NewLine + txtPending.Text;
                    if (MoveFile(queFile[i], queDir[i]))
                    {
                        queDir.RemoveAt(i);
                        queFile.RemoveAt(i);
                        i--;
                    }
                }
            }
            else
            {
                this.lblPending.Text = "No files pending";
            }
        }

        private static Boolean MoveFile(string path, string movePath)
        {
            try
            {
                if (File.Exists(path))
                {
                    string fileName = path.Substring(path.LastIndexOf("\\") + 1);
                    string fileExt = fileName.Substring(fileName.LastIndexOf("."));
                    fileName = fileName.Substring(0, fileName.LastIndexOf("."));
                    if (!Directory.Exists(movePath)) Directory.CreateDirectory(movePath);
                    string path2 = movePath + "\\" + fileName + fileExt;
                    for (int i = 1; File.Exists(path2); i++)
                    {
                        path2 = movePath + fileName + " (" + i.ToString() + ")" + fileExt;
                    }
                    if (File.GetLastWriteTime(path).AddSeconds(10) < DateTime.Now) //moving file only if last write was 10 seconds ago or farther
                    {
                        File.Move(path, path2);
                        Form1.LastInstance.txtMoved.Text = Timestamp() + "Successfully moved " + fileName + fileExt + Environment.NewLine + Form1.LastInstance.txtMoved.Text;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Form1.LastInstance.txtMoved.Text = Timestamp() + "Move failed!" + ex.HResult.ToString() + ex.ToString() + Environment.NewLine + Form1.LastInstance.txtMoved.Text;
            }
            return false;
        }

        private static string Timestamp()
        {
            return "[" + DateTime.Now.ToString("HH:mm:ss") + "] ";
        }

        private void myNotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            statusToolStripMenuItem_Click(sender, e);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                //myNotifyIcon.Visible = true;
            }
        }

        private void statusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.BringToFront();
            //myNotifyIcon.Visible = false;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormSettings.curForm == null)
            {
                Form frmSettings = new FormSettings();
                frmSettings.Show();
            }
            else 
            {
                FormSettings.curForm.Show();
                FormSettings.curForm.BringToFront();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void loadSettings()
        {
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            myNotifyIcon.Visible = false;
        }

        public void SetTooltip()
        {
            string tooltip = Application.ProductName + " version " + Application.ProductVersion;
            if (Watcher.AllOfThem.Count > 0)
                tooltip += Environment.NewLine + "Directories watching: " + Watcher.AllOfThem.Count.ToString();
            myNotifyIcon.Text = tooltip;
        }

        private void StartupTimer_Tick(object sender, EventArgs e)
        {
            this.Hide();
            ((Timer)sender).Stop();
            ((Timer)sender).Dispose();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            settingsToolStripMenuItem_Click(sender, e);
        }
    }
}
