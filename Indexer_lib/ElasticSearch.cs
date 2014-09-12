using System;
using System.Collections.Generic;
using System.Linq;
using Indexer_lib.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Indexer_lib
{
    /// <summary>
    /// Elastic Search Client that contains methods to add and update indices on an ES instance
    /// </summary>
    public class ElasticSearchClient
    {
        public ServerInfo ServerInfo { get; set; }
        public ElasticSearchClient(ServerInfo serverInfo)
        {
            this.ServerInfo = serverInfo;
        }
        private string GetRequestUrl(IFileData fileData)
        {
            return "http://" + ServerInfo + "/" + fileData.FileType.Index + "/" + fileData.FileType.Category +
                   "/" +
                   fileData.Checksum;
        }
        /// <summary>
        /// Adds a file on ES,if the file exists it's path is updated to include the new location
        /// </summary>
        /// <param name="fileData">The file to index</param>
        public void Index(IFileData fileData)
        {
            if (CheckExists(fileData))
            {
                UpdatePath(fileData);
            }
            else
            {
                 AddNew(fileData);
            }
           
        }

        /// <summary>
        /// Retrives a file from ES by it's checksum
        /// </summary>
        /// <param name="checkSum">File's checksum</param>
        /// <returns>dynamic variable that represents the file</returns>
        public dynamic Retreive(string checkSum)
        {
            string query =String.Format(@"{{
   ""query"": {{
      ""query_string"": {{
          ""query"": ""{0}"",
            ""fields"": [""_id""]
      }}
   }}
}}",checkSum);
            var elasticSearchNetwork = new ElasticSearchNetwork("http://" + ServerInfo+"/" +"_search", "POST",query);
            dynamic result=JObject.Parse(elasticSearchNetwork.SendAndGetResponse());
            if (result.hits.total.Value > 0)
            {
                return result.hits.hits[0];
            }
            return null;

        }
        /// <summary>
        /// Deletes a specific file index from ES
        /// </summary>
        /// <param name="fileData">The file to delete</param>
        public void Delete(IFileData fileData)
        {
            string requestUrl = GetRequestUrl(fileData);
            var elasticSearchNetwork = new ElasticSearchNetwork(requestUrl, "DELETE");
            elasticSearchNetwork.Send();
        }

       
        /// <summary>
        /// Adds a file index to ES
        /// </summary>
        /// <param name="fileData">The file to add</param>
        private void AddNew(IFileData fileData)
        {
            string requestUrl = GetRequestUrl(fileData);
            var elasticSearchNetwork=new ElasticSearchNetwork(requestUrl,"PUT",fileData.ToString());
            elasticSearchNetwork.SendAndGetResponse();
         
        }
        /// <summary>
        /// Checks if a certain file exists on ES
        /// </summary>
        /// <param name="fileData">The file to check</param>
        /// <returns>True if the file exists, otherwise False</returns>
        public bool CheckExists(IFileData fileData)
        {
            string requestUrl = GetRequestUrl(fileData);
            var elasticSearchNetwork = new ElasticSearchNetwork(requestUrl, "GET");
            try
            {
                elasticSearchNetwork.Send();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }

        }
        /// <summary>
        /// Gets the current path of an indexed file
        /// </summary>
        /// <param name="fileData">The file to get the current path of</param>
        /// <returns>A list of the Current paths</returns>
        private List<string> GetCurrentPath(IFileData fileData)
        {
            string requestUrl = GetRequestUrl(fileData);
            var elasticSearchNetwork = new ElasticSearchNetwork(requestUrl, "GET");
            var responseText = elasticSearchNetwork.SendAndGetResponse();
            dynamic responseJson = JObject.Parse(responseText);
            var paths = new List<string>();
            foreach (var path in responseJson._source.Path)
            {
                paths.Add(path.Value);
            }
            return paths;

        }

        /// <summary>
        /// Adds the new path to the indexed path of a file
        /// </summary>
        /// <param name="fileData">The file to update</param>
        private void UpdatePath(IFileData fileData)
        {
            string requestUrl = GetRequestUrl(fileData) + "/_update";
            var paths = GetCurrentPath(fileData);
            paths.AddRange(fileData.Path);
            var pathsString = JsonConvert.SerializeObject(paths.Distinct());
            var requestText = String.Format("{{\"doc\":{{\"Path\":{0}}}}}",pathsString);
            var elasticSearchNetwork = new ElasticSearchNetwork(requestUrl, "POST", requestText);
            elasticSearchNetwork.SendAndGetResponse();
            

        }
    }

}
