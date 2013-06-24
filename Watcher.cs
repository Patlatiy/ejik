using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace Ejik
{
    public class Watcher
    {
        private string watchPath;
        private string movePath;
        private string[] filters;
        private FileSystemWatcher watcher = new FileSystemWatcher();

        public static List<Watcher> AllOfThem = new List<Watcher>();
        public string WatchPath
        {
            get { return watchPath; }
            set 
            { 
                watchPath = watcher.Path = value;
                ScanDirectory(watchPath, this.filters);
            }
        }

        public string MovePath
        {
            get { return movePath; }
            set { movePath = value; }
        }

        public string filter
        {
            get
            {
                string tmp = "";
                for (int i = 0; i <= filters.Length - 1; i++)
                {
                    tmp += '.' + filters[i] + '|';
                }
                tmp = tmp.Remove(tmp.Length - 1);
                return tmp;
            }
            set { this.filters = parseFilter(value); }
        }

        public Watcher(string wPath, string mPath, string filter)
        {
            filters = parseFilter(filter);
            MovePath = mPath;
            WatchPath = wPath;

            //configuring and starting watcher:
            //watcher.Path is already set in WatchPath property (see above)
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;
            AllOfThem.Add(this);
            
        }

        private string[] parseFilter(string filter)
        {
            List<string> result = new List<string>();
            int j = 0;

            for (int i = 0; i < filter.Length - 1; i++)
            {
                if (filter[i] == '.')
                {
                    i++;
                    while (i < filter.Length && filter[i] != '|')
                    {
                        if (result.Count <= j)
                        {
                            result.Add(new string(filter[i],1));
                        }
                        else
                        {
                            result[j] += filter[i];
                        }
                        i++;
                    }
                    j++;
                }
            }

            string[] result2 = result.ToArray();
            return result2;
        }

        public static Boolean ExtMatch(string fileName, string[] filters)
        {
            string ext = fileName.Substring(fileName.LastIndexOf(".") + 1).ToLower();
            foreach (string filter in filters)
            {
                if (ext == filter) return true;
            }
            return false;
        }

        private void OnChanged(Object source, FileSystemEventArgs e)
        {
            if (e.Name.LastIndexOf(".") != -1 && ExtMatch(e.Name, this.filters) && !Form1.qq.Contains(e.FullPath))
            {
                Form1.qq.Add(e.FullPath);
            }
        }

        private static void ScanDirectory(string dir, string[] filters)
        {
            //searching for files that already exist:
            foreach (string fileName in Directory.EnumerateFiles(dir))
            {
                if (ExtMatch(fileName, filters))
                    Form1.qq.Add(fileName);
            }
        }

        public static void SaveToSettings()
        {
            //this is pretty dumb but I still don't know how to do it better
            string[] blankStringArray = new string[64];
            for (int i = 0; i <= 63; i++)
            {
                blankStringArray[i] = "";
            }
            blankStringArray.CopyTo(Form1.MySettings.WatchPaths, 0);
            blankStringArray.CopyTo(Form1.MySettings.MovePaths, 0);
            blankStringArray.CopyTo(Form1.MySettings.Filters, 0);

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
            AllOfThem.Clear();


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
            filters = null;
        }
    }
}
