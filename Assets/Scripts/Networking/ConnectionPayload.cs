namespace LetsDoThis.Networking
{
    internal class ConnectionPayload
    {
        public ConnectionPayload()
        {
        }

        public string clientGUID { get; set; }
        public int clientScene { get; set; }
        public string playerName { get; set; }
    }
}