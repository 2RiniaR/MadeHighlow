using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.RemoveComponent
{
    public record RemoveComponentAction : IValidatable
    {
        ISimulatable IValidatable.Validate(in IActionContext context)
        {
            return Validate(context);
        }

        [NotNull]
        public RemoveComponentResult Validate([NotNull] in IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}