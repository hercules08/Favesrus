using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Favit.Server.Controllers
{
    public class PayController : ApiController
    {
        private static string theToken;

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

            theToken = responseFromServer;
        }

        public PayController()
        {
            GetToken();
        }


        private string key = "f755502720a4838f3d93e2b17ec42779";
        private string secret;
        private string token;
        private string actualToken = "191462791bdc6f25d1689cd3db40ba";
        private string accountId = "4faffa4cca8b4dd495f28f4739dc38ce";
        private string cardNumber = "7519049210952012";

        // GET api/pay
        public IEnumerable<string> Get()
        {

            GetToken();

            var url = "https://api.sbx.gomo.do/YiiModo/api_v2/gift/send";

            WebClient client = new WebClient();

            NameValueCollection collection = new NameValueCollection()
            {
                {"consumer_key", key},
                {"access_token", "191462791bdc6f25d1689cd3db40ba"},
                {"account_id", accountId },
                {"giver_name", "Damola"},
                {"gift_amount", "50"},
                {"receiver_phone", "3014373223"}
            };

            byte[] responseArray = client.UploadValues(url, collection);

            var responseString = Encoding.ASCII.GetString(responseArray);

            return new string[] { "value1", "value2" };
        }

        // GET api/pay/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/pay
        public void Post([FromBody]string value)
        {

        }

        // PUT api/pay/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/pay/5
        public void Delete(int id)
        {
        }
    }
}
