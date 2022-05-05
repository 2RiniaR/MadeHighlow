using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record UnitEnsuredID : EntityEnsuredID
    {
        [CanBeNull]
        public new Unit Get([NotNull] in World world)
        {
            return Entity.All(world).Find(entity => entity.EnsuredID == this) as Unit;
        }
    }
}