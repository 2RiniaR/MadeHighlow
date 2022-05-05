using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record ComponentEnsuredID
    {
        public ID Content { get; init; } = ID.None;

        [CanBeNull]
        public Component Get([NotNull] in World world)
        {
            return Component.All(world).Find(item => item.EnsuredID == this);
        }

        [NotNull]
        public World Delete([NotNull] in World world)
        {
            throw new NotImplementedException();
        }
    }
}