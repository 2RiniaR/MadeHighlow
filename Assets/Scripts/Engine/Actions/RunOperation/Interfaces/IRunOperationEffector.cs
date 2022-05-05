using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     命令を実行するアクションに対して、結果に影響を与えるもの
    /// </summary>
    public interface IRunOperationEffector
    {
        /// <summary>
        ///     命令を実行するアクションに対して与える影響を返す
        /// </summary>
        public RunOperationEffect EffectOnRunOperation(
            [NotNull] in IActionContext context,
            [NotNull] in RunOperationAction action
        );
    }
}