namespace LetsDoThis.Networking
{
    public class PlayerData
    {
        private string playerName;
        private ulong localClientId;

        public PlayerData(string playerName, ulong localClientId)
        {
            this.playerName = playerName;
            this.localClientId = localClientId;
        }

        public ulong ClientId { get; internal set; }
    }
}