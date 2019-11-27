using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Configuration;

namespace EDPCheck.Helper
{
    public class XmlHelper
    {
        public void LoadXML(string path) 
        {
            ConfigXmlDocument xml = new ConfigXmlDocument();
            xml.Load("config.xml");
            XmlNode node = xml.SelectSingleNode("/configuration/appSettings");
            EDPCheckHelper.st.STID = Convert.ToInt32(node.SelectSingleNode("st").Attributes["value"].Value);
            EDPCheckHelper.st.zctime = Convert.ToInt32(node.SelectSingleNode("zctime").Attributes["value"].Value);
            EDPCheckHelper.st.gztime = Convert.ToInt32(node.SelectSingleNode("gztime").Attributes["value"].Value);
            EDPCheckHelper.st.dd = node.SelectSingleNode("dd").Attributes["value"].Value;
            EDPCheckHelper.st.ph = node.SelectSingleNode("admin").Attributes["value"].Value;
            var ips = xml.SelectSingleNode("/configuration/Check/PC");
            foreach (XmlNode xn in ips.ChildNodes)
            {
                EDPCheckHelper.EDPMsg em = new EDPCheckHelper.EDPMsg();
                em.MacName = xn.Attributes["key"].Value;
                em.IP = xn.Attributes["value"].Value;
                em.MsgTime = DateTime.Now;
                em.succes = true;
                em.Type = Convert.ToInt32(xn.Attributes["type"].Value);
                EDPCheckHelper.ems.Add(em.MacName, em);
            }
        }
    }
}
