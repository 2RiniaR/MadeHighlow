using System;

namespace RineaR.MadeHighlow
{
    public record LeavePlayerAction : Action<LeavePlayerResult>
    {
        public override LeavePlayerResult Validate(IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
