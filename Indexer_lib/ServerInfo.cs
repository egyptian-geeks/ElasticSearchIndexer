namespace Indexer_lib
{
    /// <summary>
    /// A wrapper for ES server ip
    /// </summary>
    public class ServerInfo
    {
        public string Ip { get; set; }
        public int Port { get; set; }

        public ServerInfo(string ip,int port)
        {
            this.Ip = ip;
            this.Port = port;

        }

        public override string ToString()
        {
            return this.Ip + ":" + this.Port;
        }
    }
}
