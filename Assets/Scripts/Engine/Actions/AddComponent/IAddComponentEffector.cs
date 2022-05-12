using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.AddComponent
{
    /// <summary>
    ///     命令を実行するアクションに対して、結果に影響を与えるもの
    /// </summary>
    public interface IAddComponentEffector
    {
        /// <summary>
        ///     命令を実行するアクションに対して与える影響を返す
        /// </summary>
        public ValueList<Interrupt<AddComponentEffect>> EffectsOnAddComponent(
            [NotNull] IActionContext context,
            [NotNull] AddComponentAction action
        );
    }
}
