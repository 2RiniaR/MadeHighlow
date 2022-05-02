using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public record TeleportAction() : Action(ActionType.Teleport)
    {
        [NotNull]
        public TeleportResult Run([NotNull] Session session)
        {
            throw new NotImplementedException();
        }
    }
}