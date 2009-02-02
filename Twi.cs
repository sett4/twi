using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Net;

namespace potwitter
{
    static class Twi
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                return;
            }

            string username = System.Configuration.ConfigurationManager.AppSettings["username"];
            string password = System.Configuration.ConfigurationManager.AppSettings["password"];
            string url = System.Configuration.ConfigurationManager.AppSettings["url"];

            Encoding enc = Encoding.UTF8;

            StringBuilder sb = new StringBuilder();
            sb.Append("status=");
            sb.Append(HttpUtility.UrlEncode(String.Join(" ", args), enc));
            byte[] data = Encoding.ASCII.GetBytes(sb.ToString());

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Credentials = new System.Net.NetworkCredential(username, password);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            System.IO.Stream stream = req.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Close();

            WebResponse res = req.GetResponse();
            System.IO.Stream resStream = res.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(resStream, enc);
            string html = sr.ReadToEnd();
            sr.Close();

            resStream.Close();
        }
    }
}