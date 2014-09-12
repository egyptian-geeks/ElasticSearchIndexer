using System.IO;
using Indexer_lib.Interfaces;

namespace Indexer_lib
{
   public class TxtFilesExtractor:IFileContentExtractor
    {
        public string Extract(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
