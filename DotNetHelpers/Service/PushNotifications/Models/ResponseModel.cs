using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DotNetHelpers.Service.PushNotifications.Models
{
    public class ResponseModel
    {
        public List<Tuple<string, string>> Result { get; } = new List<Tuple<string, string>>();
        public int TotalSuccess { get; }
        public int TotalFailed { get; set; }
        public string MultiCastID { get; }

        public ResponseModel(string Response)
        {
            var decodedResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(Response);
            if (decodedResponse.ContainsKey("multicast_id"))
                this.MultiCastID = decodedResponse["multicast_id"].ToString();
            if (decodedResponse.ContainsKey("success"))
                this.TotalSuccess = int.Parse(decodedResponse["success"].ToString());
            if (decodedResponse.ContainsKey("failure"))
                this.TotalFailed = int.Parse(decodedResponse["failure"].ToString());
            List<Tuple<string, object>> mResult = JsonConvert.DeserializeObject<List<Tuple<string, object>>>(decodedResponse["results"].ToString());

            foreach (var item in mResult)
                this.Result.Add(new Tuple<string, string>(item.Item1, item.Item2.ToString()));
        }
    }
}