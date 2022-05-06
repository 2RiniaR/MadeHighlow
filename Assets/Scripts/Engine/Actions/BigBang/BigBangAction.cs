using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record BigBangAction([NotNull] in World World) : Action<BigBangResult>
    {
        public override BigBangResult Validate(in IActionContext context)
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