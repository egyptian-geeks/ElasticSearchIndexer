using System.Collections.Generic;
using Indexer_lib.Interfaces;

namespace indexer_lib_test
{
    class TxtFileTypeTest:IFileType
    {
        public IFileContentExtractor FileContentExtractor
        {
            get {return null;}
        }

        public string Index
        {
            get { return "testdocuments"; }
        }

        public string Category
        {
            get { return "testtext"; }
        }

        public List<string> ExtensionsList
        {
            get { return new List<string>{"txt"};}
        }
    }
}
