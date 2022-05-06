using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     命令を実行するアクションに対して、結果に影響を与えるもの
    /// </summary>
    public interface IRunCommandEffector
    {
        /// <summary>
        ///     命令を実行するアクションに対して与える影響を返す
        /// </summary>
        public RunCommandEffect EffectOnRunCommand(
            [NotNull] in IActionContext context,
            [NotNull] in RunCommandAction action
        );
    }
}