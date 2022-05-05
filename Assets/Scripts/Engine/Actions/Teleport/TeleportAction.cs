using System;

namespace RineaR.MadeHighlow
{
    public record TeleportAction : Action<TeleportResult>
    {
        public override TeleportResult Validate(in IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}