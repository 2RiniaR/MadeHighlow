using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record RemoveComponentAction : Action<RemoveComponentResult>
    {
        [NotNull]
        public override RemoveComponentResult Validate([NotNull] in IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}