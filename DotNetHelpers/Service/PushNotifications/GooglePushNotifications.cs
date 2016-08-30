using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DotNetHelpers.Service.PushNotifications
{
    public class GooglePushNotifications
    {
        public string ServerID { get; set; }

        /// <summary>
        /// Create a new instance of <see cref="GooglePushNotification" /> class
        /// </summary>
        /// <param name="_ServerID">Authorization Key from Google Developer Console</param>
        public GooglePushNotifications(string _ServerID)
        {
            this.ServerID = _ServerID;
        }

        /// <summary>
        /// Sends push notificaiton to devices
        /// </summary>
        /// <param name="DeviceIDs">ID of devices to send to</param>
        /// <param name="PayLoad"></param>
        /// <returns></returns>
        public Models.ResponseModel SendPush(string[] DeviceIDs, Dictionary<string, string> PayLoad)
        {
            try
            {
                HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("https://android.googleapis.com/gcm/send");

                Request.Method = "POST";

                string postData = "{ \"registration_ids\": [" + string.Join(",", DeviceIDs) + "], \"data\": " + JsonConvert.SerializeObject(PayLoad) + "}";

                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                Request.ContentType = "application/json";
                Request.Headers = new WebHeaderCollection();
                Request.Headers[HttpRequestHeader.Authorization] = $"key={this.ServerID}";

                //-- Create Stream to Write Byte Array --//
                using (Stream dataStream = Request.GetRequestStreamAsync().Result)
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Flush();
                }

                //-- Post a Message --//
                using (WebResponse Response = Request.GetResponseAsync().Result)
                using (StreamReader Reader = new StreamReader(Response.GetResponseStream()))
                {
                    return new Models.ResponseModel(Reader.ReadLine());
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
