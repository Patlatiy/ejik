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
    class Watcher
    {
        private string watchPath;
        private string movePath;
        private string[] filters;
        private FileSystemWatcher watcher = new FileSystemWatcher();

        public Watcher(string wPath, string mPath, string filter)
        {
            watchPath = wPath;
            movePath = mPath;
            filters = parseFilter(filter);

            //configuring and starting watcher:
            watcher.Path = Application.StartupPath;
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;
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

        private static void OnChanged(Object source, FileSystemEventArgs e)
        {
            if (e.Name.LastIndexOf(".") != -1 && IsPicture(e.Name) && !Form1.qq.Contains(e.FullPath))
            {
                Form1.qq.Add(e.FullPath);
            }
        }
    }
}
