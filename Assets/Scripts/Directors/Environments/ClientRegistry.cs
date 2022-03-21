using System.Collections.Generic;
using RineaR.MadeHighlow.Engine.Subjects.Players;

namespace RineaR.MadeHighlow.Directors.Environments
{
    public class ClientRegistry
    {
        private Dictionary<PlayerLocator, IClient> Clients { get; } = new();

        public void SetClient(PlayerLocator player, IClient client)
        {
            Clients.Add(player, client);
        }

        public IClient ClientOf(PlayerLocator playerLocator)
        {
            return Clients.TryGetValue(playerLocator, out var client) ? client : null;
        }
    }
}