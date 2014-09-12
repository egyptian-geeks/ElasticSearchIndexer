using System.Collections.Generic;
using System.IO;
using Indexer_lib.Interfaces;

namespace Indexer_lib
{
    /// <summary>
    /// A utility class that contains functions that are related to files and directories operations
    /// </summary>
    public static class FileOperation
    {
        
        /// <summary>
        /// Enumerates the files in a given path
        /// </summary>
        /// <param name="dirPath">The path of the folder to extract files from</param>
        /// <param name="types">A list of files types to extract</param>
        /// <param name="recurse">Optional,if true search will include subfolders,default is true </param>
        /// <returns></returns>
        public static IEnumerable<FileData> EnumerateFile(string dirPath, IEnumerable<IFileType> types,bool recurse=true)
        {
            foreach (var file in Directory.EnumerateFiles(dirPath, "*.*",recurse? SearchOption.AllDirectories:SearchOption.TopDirectoryOnly))
            {

                foreach (var t in types)
                {
                    foreach (var ext in t.ExtensionsList)
                    {
                        if (file.ToLowerInvariant().EndsWith(ext))
                        {
                            yield return new FileData(file, t);
                        }
                    }
                }



            }

        }
    }

}
