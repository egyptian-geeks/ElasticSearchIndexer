using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Indexer_lib
{
    /// <summary>
    /// A utility class that contain functions to calculates the md5 checksum of files
    /// </summary>
    public static class CheckSum
    {
        /// <summary>
        /// Returns the md5 checksum of a file
        /// </summary>
        /// <param name="path">Path to the file</param>
        /// <returns>md5 checksum</returns>
        public static string Calculate(string path)
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(File.OpenRead(path));
                var sb = new StringBuilder();
                foreach (byte t in hash)
                {
                    sb.Append(t.ToString("X2"));
                }
                return sb.ToString();
            }

        }
    }
}
