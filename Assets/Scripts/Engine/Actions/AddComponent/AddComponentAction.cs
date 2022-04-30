using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.AddComponent
{
    /// <summary>
    ///     ユニットが自身にエフェクトを追加する
    /// </summary>
    public record AddComponentAction() : Action(ActionType.AddComponent)
    {
        [NotNull] public ObjectLocator ObjectLocator { get; init; } = new();
        [NotNull] public Component Component { get; init; }

        public Event Run(in Session session)
        {
            return new AddComponentEvent { ObjectLocator = ObjectLocator, Component = Component };
        }
    }
}