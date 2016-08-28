using System.Collections.Generic;
using System.Net;
using System.ServiceModel.Channels;
using System.Text;
using Newtonsoft.Json;

namespace DotNetHelpers
{
    /// <summary>
    /// Provides functions for use with <see cref="RequestContext"/>
    /// </summary>
    public static class ServiceHelper
    {
        /// <summary>
        /// Extracts Authentication Header from a request
        /// </summary>
        /// <param name="mContext">HTTP Web Request to extract header</param>
        /// <returns>Value of Authentication header</returns>
        public static string ExtractAuthorizationHeader(RequestContext mContext)
        {
            var message = mContext.RequestMessage;
            var request = (HttpRequestMessageProperty) message.Properties[HttpRequestMessageProperty.Name];
            return request.Headers[HttpRequestHeader.Authorization];
        }

        /// <summary>
        /// Extracts all values in a request header
        /// </summary>
        /// <param name="mContext">HTTP Web Request to extract Header</param>
        /// <returns>Dictionary containing header keys and values</returns>
        public static Dictionary<string, string> ExtracRequestHeaders(RequestContext mContext)
        {
            var message = mContext.RequestMessage;
            var request = (HttpRequestMessageProperty) message.Properties[HttpRequestMessageProperty.Name];
            var requestHeader = new Dictionary<string, string>();
            foreach (var item in request.Headers.Keys)
            {
                requestHeader.Add(item.ToString(), request.Headers.Get(item.ToString()));
            }
            return requestHeader;
        }

        /// <summary>
        /// Extract request body
        /// </summary>
        /// <param name="mContext">Request Context</param>
        /// <returns>Request Body</returns>
        public static Dictionary<string, string> ExtractBody(RequestContext mContext)
        {
            var body = mContext.RequestMessage.GetBody<byte[]>();
            string str = Encoding.UTF8.GetString(body);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(str);
        }

        /// <summary>
        /// Extracts request body and returns it as JSON String
        /// </summary>
        /// <param name="mContext">Request Context</param>
        /// <returns>Request Body info</returns>
        public static T ExtractBodyToJSON<T>(RequestContext mContext)
        {
            var body = mContext.RequestMessage.GetBody<byte[]>();
            string str = Encoding.UTF8.GetString(body);
            return JsonConvert.DeserializeObject<T>(str);
        }

        /// <summary>
        /// Extract RequestContext header
        /// </summary>
        /// <param name="mContext">Context to extract header</param>
        /// <param name="Header">Key of header to extract</param>
        /// <returns><see cref="string"/> value of header</returns>
        public static string ExtractHeader(RequestContext mContext, string Header)
        {
            var message = mContext.RequestMessage;
            var request = (HttpRequestMessageProperty) message.Properties[HttpRequestMessageProperty.Name];
            return request.Headers[Header];
        }
    }
}