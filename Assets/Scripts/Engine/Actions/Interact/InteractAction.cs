using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    /// <summary>
    ///     相互作用の発生
    /// </summary>
    public abstract record InteractAction() : Action(ActionType.Interact)
    {
        [NotNull] public ObjectLocator Actor { get; init; } = new();
    }
}