using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     相互作用の発生
    /// </summary>
    public abstract record InteractAction : Action<InteractResult>
    {
        [NotNull] public EntityID Actor { get; init; } = new();


        [NotNull]
        public override InteractResult Validate([NotNull] in IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}