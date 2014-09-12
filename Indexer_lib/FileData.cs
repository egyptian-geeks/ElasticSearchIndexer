using System;
using System.Collections.Generic;
using System.IO;
using Indexer_lib.Interfaces;
using Newtonsoft.Json;

namespace Indexer_lib
{
    public class FileData : IFileData
    {
        [JsonIgnore]
        public string Checksum { get; set; }
        public string Content { get; set; }
        public List<string> Path { get; set; }
        public DateTime CreateDate { get; set; }
        [JsonIgnore]
        public IFileType FileType { get; set; }
        public FileData(string path,IFileType fileType)
        {
            this.FileType = fileType;
            var fileInfo = new FileInfo(path);
            this.CreateDate = fileInfo.CreationTime;
            this.Path=new List<string> {fileInfo.FullName};
            this.Checksum = CheckSum.Calculate(path);
            if (fileType.FileContentExtractor!=null)
            {
                this.Content = fileType.FileContentExtractor.Extract(path);
            }
        }

        public override string ToString()
        {
           return JsonConvert.SerializeObject(this);
        }
    }
}
