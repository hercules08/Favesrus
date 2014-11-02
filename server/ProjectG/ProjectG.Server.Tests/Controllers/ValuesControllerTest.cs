using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectG.Server;
using ProjectG.Server.Controllers;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Web.Helpers;

namespace ProjectG.Server.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
    {

        private string key = "f755502720a4838f3d93e2b17ec42779";
        private string secret;
        private string token;
        private string actualToken = "191462791bdc6f25d1689cd3db40ba";

        public ValuesControllerTest()
        {

        }


        private void GetToken()
        {
            var key = "f755502720a4838f3d93e2b17ec42779";
            var apiSecret = "3a2e4e83550306c2f070dd5e7afffcae033e22cde7f69cdd9e86ee4d1a844e6f";
            var encodedPostData = "credentials=" + Convert.ToBase64String(Encoding.UTF8.GetBytes(key + ":" + apiSecret));
            var url = "https://api.sbx.gomo.do/YiiModo/api_v2/token";

            WebRequest request = WebRequest.Create(url);
            byte[] byteArray = Encoding.UTF8.GetBytes(encodedPostData);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response = request.GetResponse();
//            Console.WriteLine(((HttpWebResponse)response).StatusDescription);


            dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();

            token = responseFromServer;
            //Console.WriteLine(responseFromServer);
            reader.Close();
            //Assert
        }

        [TestMethod]
        public void Get()
        {
            // Arrange
            ValuesController controller = new ValuesController();

            // Act
            IEnumerable<string> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("value1", result.ElementAt(0));
            Assert.AreEqual("value2", result.ElementAt(1));
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            ValuesController controller = new ValuesController();

            // Act
            string result = controller.Get(5);

            // Assert
            Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            ValuesController controller = new ValuesController();

            // Act
            controller.Post("value");

            // Assert
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            ValuesController controller = new ValuesController();

            // Act
            controller.Put(5, "value");

            // Assert
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            ValuesController controller = new ValuesController();

            // Act
            controller.Delete(5);

            // Assert
        }

        [TestMethod]
        public void Can_Get_Token_From_Modo()
        {
            //Arrange
            var keyBox = "f755502720a4838f3d93e2b17ec42779";
            var apiSecret = "3a2e4e83550306c2f070dd5e7afffcae033e22cde7f69cdd9e86ee4d1a844e6f";
            var encodedPostData =  "credentials="+Convert.ToBase64String(Encoding.UTF8.GetBytes(keyBox + ":" + apiSecret));
            var url = "https://api.sbx.gomo.do/YiiModo/api_v2/token";

            //Act
            WebRequest request = WebRequest.Create(url);
            byte[] byteArray = Encoding.UTF8.GetBytes(encodedPostData);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);


            dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();
            Console.WriteLine (responseFromServer);
            reader.Close();
            //Assert
        }
        
        [TestMethod]
        public void Can_Register_User_With_Modo()
        {
            GetToken();

            var url = "https://api.sbx.gomo.do/YiiModo/api_v2/people/register";

            WebClient client = new WebClient();

            NameValueCollection collection = new NameValueCollection()
            {
                {"consumer_key", key},
                {"access_token", GetTokenFromJSON(token)},
                {"phone", "3014373223"},
                {"should_send_password", "0"},
                {"should_send_modo_descript", "1"},
                {"verify_password_url", "http://damolaomotosho.com/"},
                {"is_modo_terms_agree", "1"}
            };

            byte[] responseArray = client.UploadValues(url, collection);

            var responseString = Encoding.ASCII.GetString(responseArray);
        }

        private string GetTokenFromJSON(string json)
        {
            dynamic data = Json.Decode(json);

            return data.response_data.access_token;
        }
    }
}
