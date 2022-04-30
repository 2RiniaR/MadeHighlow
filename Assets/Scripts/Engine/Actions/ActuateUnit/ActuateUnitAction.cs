using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ActuateUnit
{
    public record ActuateUnitAction() : Action(ActionType.ActuateUnit)
    {
        [NotNull]
        [ItemNotNull]
        public ValueObjectList<ObjectLocator> Units { get; init; } = ValueObjectList<ObjectLocator>.Empty;

        public Event Run(in Session session)
        {
            throw new NotImplementedException();
        }
    }
}