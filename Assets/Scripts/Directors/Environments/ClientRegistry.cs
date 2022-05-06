using System.Collections.Generic;

namespace RineaR.MadeHighlow.Directors.Environments
{
    public class ClientRegistry
    {
        private Dictionary<PlayerID, IClient> Clients { get; } = new();

        public void SetClient(PlayerID player, IClient client)
        {
            Clients.Add(player, client);
        }

        public IClient ClientOf(PlayerID playerLocator)
        {
            return Clients.TryGetValue(playerLocator, out var client) ? client : null;
        }
    }
}