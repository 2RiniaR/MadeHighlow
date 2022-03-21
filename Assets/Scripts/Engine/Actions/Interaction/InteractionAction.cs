using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Subjects.Objects;

namespace RineaR.MadeHighlow.Engine.Actions.Interaction
{
    /// <summary>
    ///     相互作用の発生
    /// </summary>
    public abstract record InteractionAction() : Action(ActionType.Interaction)
    {
        [NotNull] public ObjectLocator Actor { get; init; } = new();
    }
}