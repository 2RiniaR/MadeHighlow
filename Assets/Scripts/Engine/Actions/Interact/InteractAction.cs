using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     相互作用の発生
    /// </summary>
    public abstract record InteractAction : IValidatable
    {
        [NotNull] public EntityEnsuredID Actor { get; init; } = new();

        ISimulatable IValidatable.Validate(in IActionContext context)
        {
            return Validate(context);
        }

        [NotNull]
        public InteractResult Validate([NotNull] in IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}