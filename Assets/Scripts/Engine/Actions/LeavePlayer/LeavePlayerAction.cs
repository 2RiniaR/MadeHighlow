using System;

namespace RineaR.MadeHighlow
{
    public record LeavePlayerAction : Action<LeavePlayerResult>
    {
        public override LeavePlayerResult Validate(in IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}