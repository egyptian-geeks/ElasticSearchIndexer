using System.Collections.Generic;
using Indexer_lib.Interfaces;

namespace Indexer_lib
{
    public class TxtFileType:IFileType
    {
        public IFileContentExtractor FileContentExtractor
        {
            get
            {
                return  new TxtFilesExtractor();
            }
            
        }

        public string Index
        {
            get
            {
                return "documents";
            }
            
        }

        public string Category
        {
            get
            {
                return "text";
            }
           
        }


        public List<string> ExtensionsList
        {
            get { return new List<string>{"txt"}; }
        }
    }
}
