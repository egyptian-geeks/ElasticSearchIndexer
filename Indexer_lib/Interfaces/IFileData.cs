using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Indexer_lib.Interfaces
{
    public interface IFileData
    {
        [JsonIgnore]
        string Checksum { get; set; }

        string Content { get; set; }
        List<string> Path { get; set; }
        DateTime CreateDate { get; set; }

        [JsonIgnore]
        IFileType FileType { get; set; }

        
    }
}