using System;
using System.Collections.Generic;
using Indexer_lib.Interfaces;
using Newtonsoft.Json;

namespace indexer_lib_test
{
    public class FileDataTest:IFileData
    {
        public string Checksum { get; set; }
        public string Content{ get; set; }
        public List<string> Path { get; set; }
        public DateTime CreateDate { get; set; }

        public IFileType FileType
        {
            get
            {
                return new TxtFileTypeTest();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
