using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Sabong.Repository;
using Sabong.Repository.Repo;


namespace Sabong.Test
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void Text1()
        {
            Repository.Repo.UserRepository user=new UserRepository();
            var xx= user.Login("t","qq123456");
        }

        [Test]
        public void PostToNode()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:8888/push");
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"user\":\"test\"," +
                              "\"password\":\"bla\"}";

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

        [Test]
        public void TestDate()
        {
            var date = DateTime.Now;
            var x = date.ToString("MMM dd, yyyy");
            Console.WriteLine(x);
        }
    }
}
