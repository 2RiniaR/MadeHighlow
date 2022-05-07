using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record BigBangAction([NotNull] World World) : Action<BigBangResult>
    {
        public override BigBangResult Validate(IActionContext context)
        {
            if (!context.Session.Events.IsEmpty)
            {
                throw new InvalidOperationException();
            }

            var generatedWorld = new WorldFormatter().Format(World);
            return new SucceedBigBangResult(generatedWorld);
        }
    }
}