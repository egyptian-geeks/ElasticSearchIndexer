using System.Collections.Generic;
using System.Threading;
using Indexer_lib;
using Indexer_lib.Interfaces;
using NUnit.Framework;

namespace indexer_lib_test
{
    [TestFixture]
    public class ElasticSearchTest
    {
        private ElasticSearchClient _elasticSearchClient;
        private IFileData _fileData;
        [TestFixtureSetUp]
        public void SetUp()
        {
            _fileData = new FileDataTest() {Checksum = "123",Content = "test",Path = new List<string>{@"c:\test\test.txt"}};
            _elasticSearchClient = new ElasticSearchClient(new ServerInfo("localhost", 9200));

        }

        [Test]
        public void ClientCanAddIndex()
        {
            _elasticSearchClient.Index(_fileData);
            var exists = _elasticSearchClient.CheckExists(_fileData);
            
            Assert.That(exists,Is.True);
        
        }

        [Test]
        public void PathAppendedForFileCopy()
        {
            _elasticSearchClient.Index(_fileData);
             string newPath = @"c:\test2\test2.txt";
            _fileData.Path = new List<string> {newPath};
            _elasticSearchClient.Index(_fileData);
            Thread.Sleep(1000);
            var index = _elasticSearchClient.Retreive(_fileData.Checksum);
            Assert.That(index._source.Path.Count==2 ,Is.True);
        }
        
        [TearDown]
        public void Clean()
        {
           
            _elasticSearchClient.Delete(_fileData);
        }
    }
}
