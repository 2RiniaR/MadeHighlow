using System;

namespace RineaR.MadeHighlow
{
    public record ElevateTileAction : Action<ElevateTileResult>
    {
        public override ElevateTileResult Validate(in IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}