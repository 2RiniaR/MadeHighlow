using System.Collections.Generic;

namespace RineaR.MadeHighlow.Directors.Environments
{
    public class ClientRegistry
    {
        private Dictionary<PlayerEnsuredID, IClient> Clients { get; } = new();

        public void SetClient(PlayerEnsuredID player, IClient client)
        {
            Clients.Add(player, client);
        }

        public IClient ClientOf(PlayerEnsuredID playerLocator)
        {
            return Clients.TryGetValue(playerLocator, out var client) ? client : null;
        }
    }
}