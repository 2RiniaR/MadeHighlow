using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public record ActuateUnitAction() : Action(ActionType.ActuateUnit)
    {
        [NotNull] public ValueObjectList<ObjectLocator> Units { get; init; } = ValueObjectList<ObjectLocator>.Empty;

        public ActuateUnitResult Run(in Session session)
        {
            throw new NotImplementedException();
        }
    }
}