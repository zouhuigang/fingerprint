using System.IO;
using System;
using System.Net;
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WindowsFormsApp1
{


  



    class http
    {
        //private string HttpsWebRoot= "http://192.168.99.100:8098/";
        private string HttpsWebRoot = "https://www.anooc.com/";

        public string SendPrint(string uid) {
            //string req = "请求返回数据:";
            long push_time  = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            string sign= Md5Hash(uid+push_time+ "win_client");
            string resultJson= HttpPost(HttpsWebRoot + "paike/student/printall", "uid="+ uid + "&push_time="+ push_time+"&sign="+sign+ "&source=win_client");
            return resultJson;
        }


        private static string Md5Hash(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }


        private string HttpPost(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            request.Proxy = null; //不使用默认代理
            //request.CookieContainer = cookie;
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //response.Cookies = cookie.GetCookies(response.ResponseUri);
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

    }
}
