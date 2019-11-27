using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDPCheck
{
    public class EDPCheckHelper
    {
        public struct EDPMsg 
        {
            public string MacName;
            public string IP;
            public DateTime MsgTime;
            public bool succes;
            public int Type;
        }

        public struct Store 
        {
            public int STID;
            public string dd;
            public string ph;
            public int zctime;
            public int gztime;
        }

        public static Dictionary<string, EDPMsg> ems = new Dictionary<string, EDPMsg>();
        public static Store st = new Store();
        public static string LocalIP;
    }
}
