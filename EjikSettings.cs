using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Ejik
{
    public class EjikSettings : ApplicationSettingsBase
    {
        [UserScopedSetting()]
        public string[] WatchPaths
        {
            get
            {
                return ((string[])this["watchpaths"]);
            }
            set
            {
                this["watchpaths"] = (string[])value;
            }
        }

        [UserScopedSetting()]
        public string[] MovePaths
        {
            get
            {
                return ((string[])this["movepaths"]);
            }
            set
            {
                this["movepaths"] = (string[])value;
            }
        }

        [UserScopedSetting()]
        public string[] Filters
        {
            get
            {
                return ((string[])this["filters"]);
            }
            set
            {
                this["filters"] = (string[])value;
            }
        }
    }
}
