using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries.Objects
{
    public record CreateObjectQuery
    {
        [CanBeNull] public Object Value { get; init; } = null;

        [NotNull]
        public World Run([NotNull] in World world)
        {
            if (Value == null) return world;
            return world with
            {
                Objects = world.Objects.Items.Add(Value).ToValueObjectList(),
            };
        }
    }
}