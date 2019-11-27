using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDPCheck
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            EDPCheck.Helper.FrmHelper.SetPenetrate(this);//设置窗体具有鼠标穿透效果
            EDPCheckHelper.LocalIP = EDPCheck.Helper.NetHelper.GetLocalIP();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (var em in EDPCheckHelper.ems)
            {
                EDPCheckHelper.EDPMsg nem = new EDPCheckHelper.EDPMsg();
                nem = EDPCheck.Helper.CheckHelper.PingMachiAsync(em.Key, em.Value).Result;
                string key = em.Key;
                EDPCheckHelper.ems.Remove(key);
                EDPCheckHelper.ems.Add(key, nem);
                switch (nem.Type)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    default:
                        break;
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

    }
}
