using System.Collections.Generic;


namespace Indexer_lib.Interfaces
{
   public interface IFileType
    {
        IFileContentExtractor FileContentExtractor { get;  }
        string Index { get;  }
        string Category { get; }
        List<string> ExtensionsList { get; } 
    }
}
