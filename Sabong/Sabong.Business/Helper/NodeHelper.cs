using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sabong.Business
{
    public class NodeHelper
    {
        private static string NodeServer
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["NodeServer"];
                }
                catch (Exception)
                {
                    return "http://localhost:8888/push?token=a69849e8-b44a-4d23-903a-c09dd46e6deb";
                }
            }
        }
        public static void SendToNode(object data)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(NodeServer);
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }
    }
}
