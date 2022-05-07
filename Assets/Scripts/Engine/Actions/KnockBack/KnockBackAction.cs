using System;

namespace RineaR.MadeHighlow
{
    public record KnockBackAction : Action<KnockBackResult>
    {
        public override KnockBackResult Validate(IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}