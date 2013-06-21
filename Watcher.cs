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
                watchPath = value;
                watcher.EnableRaisingEvents = false;
                watcher.Path = value;
                watcher.EnableRaisingEvents = true;
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
            watchPath = wPath;
            movePath = mPath;
            filters = parseFilter(filter);

            //configuring and starting watcher:
            watcher.Path = watchPath;
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

        public static Boolean IsPicture(string fileName)
        {
            string ext = fileName.Substring(fileName.LastIndexOf(".")).ToLower();
            if (ext == ".jpg" | ext == ".jpeg" | ext == ".gif" | ext == ".png" | ext == ".bmp")
            {
                return true;
            }
            return false;
        }

        public static Boolean ExtMatch(string fileName, string[] filters)
        {
            string ext = fileName.Substring(fileName.LastIndexOf(".")).ToLower();
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
    }
}
