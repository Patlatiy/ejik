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

namespace Ejik
{
    public partial class Form1 : Form
    {
        public static List<String> qq = new List<string>();
        public static Label lbl1;
        public static Label lbl2;

        public Form1()
        {
            InitializeComponent();
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private void Form1_Load(object sender, EventArgs e)
        {
            //configuring and starting watcher:
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = Application.StartupPath;
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;

            //searching for pictures that are already exists:
            foreach (string fileName in Directory.EnumerateFiles(Application.StartupPath))
            {
                if (IsPicture(fileName))
                    qq.Add(fileName);
            }

            //poekhali!
            moveTimer.Start();

            //looks like this is needed for logging
            lbl1 = this.label1;
            lbl2 = this.label2;
        }

        private static Boolean IsPicture(string fileName)
        {
            string ext = fileName.Substring(fileName.LastIndexOf(".")).ToLower();
            if (ext == ".jpg" | ext == ".jpeg" | ext == ".gif" | ext == ".png" | ext == ".bmp")
            {
                return true;
            }
            return false;
        }

        private static void OnChanged(Object source, FileSystemEventArgs e)
        {
            if (e.Name.LastIndexOf(".") != -1 && IsPicture(e.Name) && !qq.Contains(e.FullPath)) 
            {
                qq.Add(e.FullPath);
            }
        }

        private void moveTimer_Tick(object sender, EventArgs e)
        {
            lbl2.Text = "Files pending:" + Environment.NewLine;
            if (qq.Count != 0)
                for (int i = 0; i < qq.Count; i++)
                {
                    lbl2.Text += qq[i].Substring(qq[i].LastIndexOf("\\") + 1) + Environment.NewLine;
                    MoveFile(qq[i]);
                }
        }

        private static void MoveFile(String path)
        {
            try
            {
                if (File.Exists(path))
                {
                    string fileName = path.Substring(path.LastIndexOf("\\") + 1);
                    string fileExt = fileName.Substring(fileName.LastIndexOf("."));
                    fileName = fileName.Substring(0, fileName.LastIndexOf("."));
                    string fPath = Application.StartupPath + "\\_jpg\\";
                    if (!Directory.Exists(fPath)) Directory.CreateDirectory(fPath);
                    string path2 = fPath + fileName + fileExt;
                    for (int i = 1; File.Exists(path2); i++)
                    {
                        path2 = fPath + fileName + " (" + i.ToString() + ")" + fileExt;
                    }
                    if (File.GetLastWriteTime(path).AddSeconds(10) < DateTime.Now) //moving file only if last write was 10 seconds ago or farther
                    {
                        File.Move(path, path2);
                        qq.Remove(path);
                        lbl1.Text = "Successfully moved " + fileName + fileExt + Environment.NewLine + lbl1.Text;
                        lbl1.Text.Remove(lbl1.Text.LastIndexOf(Environment.NewLine));
                    }
                }
            }
            catch (Exception ex)
            {
                lbl1.Text = "Move failed!" + ex.HResult.ToString() + ex.ToString() + Environment.NewLine + lbl1.Text;
                lbl1.Text.Remove(lbl1.Text.LastIndexOf(Environment.NewLine));
            }
        }
    }
}
