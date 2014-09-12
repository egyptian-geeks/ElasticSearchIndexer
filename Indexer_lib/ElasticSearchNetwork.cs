using System.IO;
using System.Net;
using System.Text;

namespace Indexer_lib
{
    /// <summary>
    /// A wrapper for Elastic Search server communication
    /// </summary>
    class ElasticSearchNetwork
    {
        public string requestUrl { get; set; }
        public string dataText { get; set; }
        public string method { get; set; }
        public ElasticSearchNetwork(string requestUrl,string method, string dataText=null)
        {
            this.requestUrl = requestUrl;
            this.method = method;
            this.dataText = dataText;
        }
        private WebRequest CreateWebRequest()
        {
            WebRequest webRequest = WebRequest.Create(requestUrl);
            webRequest.Method = method;
            webRequest.ContentType = "json";
            if (!string.IsNullOrEmpty(dataText))
            {
                var data = UTF8Encoding.UTF8.GetBytes(dataText);
                var requestStream = webRequest.GetRequestStream();
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();
            }
            return webRequest;
        }
        public string SendAndGetResponse()
        {
            var webRequest = CreateWebRequest();
            var response = webRequest.GetResponse();
            return new StreamReader(response.GetResponseStream()).ReadToEnd();
        }

        public void Send()
        {
            var webRequest = CreateWebRequest();
            webRequest.GetResponse();
            
        }
    }
}
