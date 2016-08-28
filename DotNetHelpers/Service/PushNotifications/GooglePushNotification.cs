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
    public class GooglePushNotification
    {
        public string ServerID { get; set; }
        /// <summary>
        /// Create a new instance of <see cref="GooglePushNotification" /> class
        /// </summary>
        /// <param name="_ServerID">Authorization Key from Google Developer Console</param>
        public GooglePushNotification(string _ServerID)
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
                Request.KeepAlive = false;

                string postData = "{ \"registration_ids\": [" + string.Join(",", DeviceIDs) + "], \"data\": " + JsonConvert.SerializeObject(PayLoad) + "}";

                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                Request.ContentType = "application/json";
                Request.Headers.Add(HttpRequestHeader.Authorization, "key=" + this.ServerID);
                
                //-- Create Stream to Write Byte Array --//
                Stream dataStream = Request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Flush();


                //-- Post a Message --//
                WebResponse Response = Request.GetResponse();
                dataStream.Close();
                StreamReader Reader = new StreamReader(Response.GetResponseStream());
                string responseLine = Reader.ReadLine();
                Reader.Close();
                return new Models.ResponseModel(responseLine);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
