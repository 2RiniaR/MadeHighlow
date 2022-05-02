using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public record ActuateUnitAction() : Action(ActionType.ActuateUnit)
    {
        [NotNull] public ValueObjectList<EntityLocator> Units { get; init; } = ValueObjectList<EntityLocator>.Empty;

        public ActuateUnitResult Run(in Session session)
        {
            throw new NotImplementedException();
        }
    }
}