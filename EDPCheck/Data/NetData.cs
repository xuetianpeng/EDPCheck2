using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDPCheck
{
    class NetData
    {
        EDPDataDataContext db = new EDPDataDataContext();

        public string GetSTName(int stid) 
        {
            var st = from s in db.T_ST where s.STID == stid select s;
            return st.FirstOrDefault().ST;
        }
        public T_ST GetSTInfo(int stid) 
        {
            var st = from s in db.T_ST where s.STID == stid select s;
            return st.FirstOrDefault();
        }
        //断线时报警
        public static void Err(EDPCheckHelper.EDPMsg edpm) 
        {
            T_ErrMsg em = new T_ErrMsg();
            em.ST = EDPCheckHelper.st.STID;
            em.ErrTime = DateTime.Now;
            em.ErrMsg = edpm.MacName + ":" + edpm.IP + "的机器报警：网络连接已中断。报警时间：" + DateTime.Now.ToString();
            EDPDataDataContext db = new EDPDataDataContext();
            db.T_ErrMsg.InsertOnSubmit(em);
            db.SubmitChanges();
        }
        public static void OK(EDPCheckHelper.EDPMsg edpm)
        {
            T_ErrMsg em = new T_ErrMsg();
            em.ST = EDPCheckHelper.st.STID;
            em.ErrTime = DateTime.Now;
            em.ErrMsg = edpm.MacName + ":" + edpm.IP + "的机器报警：网络连接已恢复。恢复时间：" + DateTime.Now.ToString();
            EDPDataDataContext db = new EDPDataDataContext();
            db.T_ErrMsg.InsertOnSubmit(em);
            db.SubmitChanges();
        }

        //断线时提示
        public static void Back(EDPCheckHelper.EDPMsg edpm)
        {
            T_Err er = new T_Err();
            er.ST = EDPCheckHelper.st.STID;
            er.MacName = edpm.MacName;
            er.MacIP = edpm.IP;
            er.ErrTime = edpm.MsgTime;
            EDPDataDataContext db = new EDPDataDataContext();
            db.T_Err.InsertOnSubmit(er);
            db.SubmitChanges();
        }
        //回复时提示
        public static void ReBack(EDPCheckHelper.EDPMsg edpm) 
        {
            EDPDataDataContext db = new EDPDataDataContext();
            var er = from e in db.T_Err where e.ST == EDPCheckHelper.st.STID && e.MacName == edpm.MacName && e.ReTime == null select e;
            er.LastOrDefault().ReTime = edpm.MsgTime;
            db.SubmitChanges();
        }
        //断线回复记录
        public static T_Err? ErrList(int stid,string MacIP) 
        {
            EDPDataDataContext db = new EDPDataDataContext();
            var te = from t in db.T_Err where t.ST == stid && t.MacIP == MacIP select t;
            return te.LastOrDefault();
        }

        //更新检查记录
        public static void MsgList(string Msg) 
        {
            EDPDataDataContext db = new EDPDataDataContext();
            var tm = from t in db.T_Msg where t.ST == EDPCheckHelper.st.STID select t;
            tm.FirstOrDefault().Msg = Msg;
            tm.FirstOrDefault().MsgTime = DateTime.Now;
            db.SubmitChanges();
        }
    }
}
