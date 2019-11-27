using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace EDPCheck.Helper
{
    public static class DingTalkHelper
    {
        private static void Post(string paraUrlCoded,string dd)
        {
            string url = dd;//@"https://oapi.dingtalk.com/robot/send?access_token=8e03a1889da305b506dcfcf70f3f5b82f273a56c71d10e2277a1d434a4a8d2bc";
            string strURL = url;
            HttpWebRequest request;
            request = (HttpWebRequest)WebRequest.Create(strURL);
            request.Method = "POST";

            //判断是否必要性
            request.ContentType = "application/json;charset=UTF-8";
            //request.ContentType = "application/json;";


            //添加cookie测试
            //Uri uri = new Uri(url);
            //Cookie cookie = new Cookie("Name", DateTime.Now.ToString()); // 设置key、value形式的Cookie
            //CookieContainer cookies = new CookieContainer();
            //cookies.Add(uri, cookie);
            //request.CookieContainer = cookies;

            //发送请求的另外形式
            //request.Method = "POST";
            //request.ContentType = "application/x-www-form-urlencoded";

            byte[] payload;
            payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
            request.ContentLength = payload.Length;
            Stream writer = request.GetRequestStream();
            writer.Write(payload, 0, payload.Length);
            writer.Close();


            System.Net.HttpWebResponse response;
            response = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.Stream s;
            s = response.GetResponseStream();
            string StrDate = "";
            string strValue = "";
            StreamReader Reader = new StreamReader(s, Encoding.UTF8);
            while ((StrDate = Reader.ReadLine()) != null)
            {
                strValue += StrDate + "\r\n";
            }

            //添加关闭相应
            Reader.Close();
            response.Close();

            //改变返回结果形式以看全部提示
            //label3.Text = strValue;
            // MessageBox.Show(strValue);
        }

        public static void RePost(string s,string ph)
        {
            string lastdd = s;
            string paraUrlCoded = "{\"msgtype\":\"text\",\"text\":{\"content\":\"";
            paraUrlCoded += s;
            paraUrlCoded += "\"},\"at\":{\"atMobiles\":[\"";
            paraUrlCoded += ph;
            paraUrlCoded += "\"],\"isAtAll\": false} }";
            Post(paraUrlCoded,EDPCheckHelper.st.dd);
        }
    }
}
