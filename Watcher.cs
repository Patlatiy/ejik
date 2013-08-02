using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Ejik
{
    public class Watcher
    {
        private string watchPath;
        private string movePath;
        private string filter;
        private FileSystemWatcher watcher = new FileSystemWatcher();

        public static List<Watcher> AllOfThem = new List<Watcher>();
        public string WatchPath
        {
            get { return watchPath; }
            set 
            {
                watchPath = watcher.Path = value.Trim('\\', ' ');
                ScanDirectory(watchPath, movePath, filter);
            }
        }

        public string MovePath
        {
            get { return movePath; }
            set { movePath = value.Trim('\\',' '); }
        }

        public string Filter
        {
            get { return filter; }
            set 
            { 
                filter = value;
                ScanDirectory(WatchPath, MovePath, filter);
            }
        }

        public Watcher(string wPath, string mPath, string filter)
        {
            this.filter = filter;
            this.movePath = mPath;
            this.watchPath = wPath;
            ScanDirectory(WatchPath, MovePath, Filter);

            //configuring and starting watcher:
            watcher.Path = wPath;
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;
            AllOfThem.Add(this);
            
        }

        private string[] parseFilter(string filter)
        {
            return filter.Split('|', ',');
        }

        private void OnChanged(Object source, FileSystemEventArgs e)
        {
            if (checkMask(e.FullPath, Filter) && !Form1.queFile.Contains(e.FullPath))
            {
                Form1.queFile.Add(e.FullPath);
                Form1.queDir.Add(this.MovePath);
            }
        }

        private static void ScanDirectory(string dir, string moveDir, string filter)
        {
            //searching for files that already exist:
            foreach (string fileName in Directory.EnumerateFiles(dir))
            {
                if (checkMask(fileName, filter) && !Form1.queFile.Contains(fileName))
                {
                    Form1.queFile.Add(fileName);
                    Form1.queDir.Add(moveDir);
                }
            }
        }

        public static void SaveToSettings()
        {
            if (Form1.MySettings.WatchPaths == null)
            {
                Form1.MySettings.WatchPaths = new string[64];
                Form1.MySettings.MovePaths = new string[64];
                Form1.MySettings.Filters = new string[64];
            }
            else
            {
                string[] blankStringArray = new string[64];
                for (int i = 0; i <= 63; i++)
                {
                    blankStringArray[i] = string.Empty;
                }
                blankStringArray.CopyTo(Form1.MySettings.WatchPaths, 0);
                blankStringArray.CopyTo(Form1.MySettings.MovePaths, 0);
                blankStringArray.CopyTo(Form1.MySettings.Filters, 0);
            }
            int index = 0;
            foreach (Watcher watcher in AllOfThem)
            {
                Form1.MySettings.WatchPaths[index] = watcher.WatchPath;
                Form1.MySettings.MovePaths[index] = watcher.MovePath;
                Form1.MySettings.Filters[index] = watcher.filter;
                index++;
            }
            Form1.MySettings.Save();
        }

        public static void LoadFromSettings()
        {
            //cleaning:
            foreach (Watcher watcher in AllOfThem)
            {
                watcher.Dispose();
            }
            //AllOfThem.Clear();


            //Watcher testWatcher = new Watcher(Application.StartupPath, Application.StartupPath + "\\_jpg\\", "*.jpg|*.jpeg|*.gif|*.png|*.bmp");

            //loading:
            for (int i = 0; i < 64; i++)
            {
                try { Watcher newWatcher = new Watcher(Form1.MySettings.WatchPaths[i], Form1.MySettings.MovePaths[i], Form1.MySettings.Filters[i]); }
                catch { break; }
            }
        }

        public void Dispose()
        {
            watchPath = null;
            movePath = null;
            filter = null;
            AllOfThem.Remove(this);
        }

        static public bool checkMask(string fileName, string input)
        {
            if (fileName == null || input == null) return false;
            string[] exts = input.Split('|', ',', ';');
            string pattern = string.Empty;
            foreach (string ext in exts)
            {
                pattern += @"^";//признак начала строки
                foreach (char symbol in ext)
                    switch (symbol)
                    {
                        case '.': pattern += @"\."; break;
                        case '?': pattern += @"."; break;
                        case '*': pattern += @".*"; break;
                        default: pattern += symbol; break;
                    }
                pattern += @"$|";//признак окончания строки
            }
            if (pattern.Length == 0) return false;
            pattern = pattern.Remove(pattern.Length - 1);
            Regex mask = new Regex(pattern, RegexOptions.IgnoreCase);
            return mask.IsMatch(System.IO.Path.GetFileName(fileName));
        }
    }
}
