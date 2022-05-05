using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record UnitEnsuredID : EntityEnsuredID
    {
        [CanBeNull]
        public Unit Get([NotNull] in World world)
        {
            return Entity.GetAllFrom(world).Find(entity => entity.EnsuredID == this) as Unit;
        }
    }
}