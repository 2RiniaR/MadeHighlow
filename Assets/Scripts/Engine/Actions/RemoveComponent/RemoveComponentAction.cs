using System;

namespace RineaR.MadeHighlow
{
    public record RemoveComponentAction : Action<RemoveComponentResult>
    {
        public override RemoveComponentResult Validate(IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}