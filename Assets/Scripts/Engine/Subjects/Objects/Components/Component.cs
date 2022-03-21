using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Subjects.Expressions;

namespace RineaR.MadeHighlow.Engine.Subjects.Objects.Components
{
    /// <summary>
    ///     「エンティティ」の効果
    /// </summary>
    /// <param name="Type">種類</param>
    public abstract record Component([NotNull] in ComponentType Type)
    {
        public ID<Component> ID { get; init; } = ID<Component>.None;
        [NotNull] public Duration Duration { get; init; } = Duration.Unlimited;

        /// <summary>種類</summary>
        [NotNull]
        public ComponentType Type { get; } = Type;
    }
}