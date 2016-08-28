using System.IO;
using System.Net;
using System.Text;

namespace DotNetHelpers.Service.PushNotifications
{
    public class ApplePushNotifications
    {
        private string ServerID { get; }

        public ApplePushNotifications(string _serverID)
        {
            this.ServerID = _serverID;
        }

        public Models.ResponseModel SendNotification(string deviceId, string message, int badgeNumber)
        {
            try
            {
                HttpWebRequest Request = (HttpWebRequest) WebRequest.Create("https://gcm-http.googleapis.com/gcm/send");
                Request.Method = "POST";
                Request.KeepAlive = false;
                string notification = "{\"sound\":\"default\",\"badge\":\"" + badgeNumber + "\",\"title\":\"EduFlag\",\"body\":\"" + message + "\"}"; // put the message you want to send here
                string messageToSend = "{\"to\":\"" + deviceId + "\",\"notification\":" + notification + ",\"content_available\":true,\"priority\":\"normal\"}"; // Construct the message.
                string postData = messageToSend;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                Request.ContentType = "application/json";
                Request.Headers.Add(HttpRequestHeader.Authorization, string.Join("=", "key", this.ServerID));
                Stream dataStream = Request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse Response = Request.GetResponse();
                HttpStatusCode ResponseCode = ((HttpWebResponse) Response).StatusCode;
                StreamReader Reader = new StreamReader(Response.GetResponseStream());
                string responseLine = Reader.ReadLine();
                Reader.Close();

                return new Models.ResponseModel(responseLine); ;
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}