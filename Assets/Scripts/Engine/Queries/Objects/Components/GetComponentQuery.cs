using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Exceptions;
using RineaR.MadeHighlow.Engine.Subjects;
using RineaR.MadeHighlow.Engine.Subjects.Objects.Components;

namespace RineaR.MadeHighlow.Engine.Queries.Objects.Components
{
    public record GetComponentQuery
    {
        [NotNull] public ComponentLocator Locator { get; init; } = new();

        [NotNull]
        public Component Run([NotNull] in World world)
        {
            return new GetObjectQuery { Locator = Locator }.Run(world)
                .Components.Find(card => card.ID == Locator.ComponentID) ?? throw new NotExistException();
        }
    }
}