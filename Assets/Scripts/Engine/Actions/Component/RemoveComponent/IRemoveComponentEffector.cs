using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RemoveComponent
{
    /// <summary>
    ///     命令を実行するアクションに対して、結果に影響を与えるもの
    /// </summary>
    public interface IRemoveComponentEffector
    {
        /// <summary>
        ///     命令を実行するアクションに対して与える影響を返す
        /// </summary>
        public ValueList<Interrupt<RemoveComponentEffect>> EffectsOnRemoveComponent(
            [NotNull] IHistory context,
            [NotNull] Component target
        );
    }
}
