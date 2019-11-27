using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;

namespace EDPCheck.Helper
{
    public class CheckHelper
    {
        static bool PingPC(string IP)
        {
            Ping pin = new Ping();
            PingReply pr = pin.Send(IP);
            return pr.Status == IPStatus.Success;
        }

        public static async Task<EDPCheckHelper.EDPMsg> PingMachiAsync(string emid,EDPCheckHelper.EDPMsg em)
        {
            await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
            if (em.succes == true)//上次通
            {
                if (PingPC(em.IP) == true)//本次通 
                {
                    em.MsgTime = DateTime.Now;
                }
                else//本次不通 
                {
                    EDPCheck.Helper.DingTalkHelper.RePost("源IP为"
                        + EDPCheckHelper.LocalIP + "的PC报警:" + em.MacName + ":" + em.IP +
                        "网络不通,请检查。ErrCode_1", EDPCheckHelper.st.ph);
                    em.succes = false;
                    em.MsgTime = DateTime.Now;
                }
            }
            else
            {
                if (PingPC(em.IP) == true)//上次不通本次通
                {
                    EDPCheck.Helper.DingTalkHelper.RePost("源IP为"
                        + EDPCheckHelper.LocalIP + "的PC报警:" + em.MacName + ":" + em.IP +
                        "网络不通,请检查。MsgCode_1", EDPCheckHelper.st.ph);
                    em.succes = true;
                    em.MsgTime = DateTime.Now;
                }
                else //上次不通本次也不通
                {
                    if ((DateTime.Now - em.MsgTime).TotalMinutes > 30) //上次报警超过30分钟
                    {
                        EDPCheck.Helper.DingTalkHelper.RePost("源IP为"
                            + EDPCheckHelper.LocalIP + "的PC报警:" + em.MacName + ":" + em.IP +
                            "网络不通,请检查。ErrCode_1", EDPCheckHelper.st.ph);
                        em.succes = false;
                        em.MsgTime = DateTime.Now;
                    }
                    else
                    {

                    }
                }

            }
            return em;
        }

        public static async Task OnWorkAsync() 
        {

        }
    }
}
