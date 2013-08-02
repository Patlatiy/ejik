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
        private static long filesMoved = 0;
       

        public Form1()
        {
            InitializeComponent();
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private void Form1_Load(object sender, EventArgs e)
        {
            LastInstance = this;
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
                    //moving file only if last write was 10 seconds ago or farther, so we don't move the files that are still being downloaded:
                    if (File.GetLastWriteTime(path).AddSeconds(10) < DateTime.Now) 
                    {
                        for (int i = 1; File.Exists(path2); i++)
                        {
                            if (FileCompare(path, path2))
                            {
                                File.Delete(path);
                                Form1.LastInstance.txtMoved.Text = Timestamp() + "Such file already exists and is equal to a new file: " + fileName + fileExt + Environment.NewLine + Form1.LastInstance.txtMoved.Text;
                                return true;
                            }
                            else
                            {
                                path2 = movePath + "\\" + fileName + " (" + i.ToString() + ")" + fileExt;
                            }
                        }
                        File.Move(path, path2);
                        Form1.LastInstance.txtMoved.Text = Timestamp() + "Successfully moved " + fileName + fileExt + Environment.NewLine + Form1.LastInstance.txtMoved.Text;
                        filesMoved++;
                        Form1.LastInstance.SetTooltip();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Form1.LastInstance.txtMoved.Text = Timestamp() + "Move failed!" + ex.HResult.ToString() + " " + ex.ToString() + Environment.NewLine + Form1.LastInstance.txtMoved.Text;
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            myNotifyIcon.Visible = false;
        }

        public void SetTooltip()
        {
            string tooltip = Application.ProductName + " version " + Application.ProductVersion;
            if (Watcher.AllOfThem.Count > 0)
                tooltip += Environment.NewLine + "Directories watching: " + Watcher.AllOfThem.Count.ToString();
            if (filesMoved != 0)
                tooltip += Environment.NewLine + "Files moved: " + filesMoved.ToString();
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

        // This method accepts two strings the represent two files to 
        // compare. A return value of 0 indicates that the contents of the files
        // are the same. A return value of any other value indicates that the 
        // files are not the same.
        private static bool FileCompare(string file1, string file2)
        {
            int file1byte;
            int file2byte;
            FileStream fs1;
            FileStream fs2;

            // Determine if the same file was referenced two times.
            if (file1 == file2)
            {
                // Return true to indicate that the files are the same.
                return true;
            }

            // Open the two files.
            fs1 = new FileStream(file1, FileMode.Open, FileAccess.Read);
            fs2 = new FileStream(file2, FileMode.Open, FileAccess.Read);

            // Check the file sizes. If they are not the same, the files 
            // are not the same.
            if (fs1.Length != fs2.Length)
            {
                // Close the file
                fs1.Close();
                fs2.Close();

                // Return false to indicate files are different
                return false;
            }

            // Read and compare a byte from each file until either a
            // non-matching set of bytes is found or until the end of
            // file1 is reached.
            do
            {
                // Read one byte from each file.
                file1byte = fs1.ReadByte();
                file2byte = fs2.ReadByte();
            }
            while ((file1byte == file2byte) && (file1byte != -1));

            // Close the files.
            fs1.Close();
            fs2.Close();

            // Return the success of the comparison. "file1byte" is 
            // equal to "file2byte" at this point only if the files are 
            // the same.
            return ((file1byte - file2byte) == 0);
        }
    }
}
