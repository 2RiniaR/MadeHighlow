using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     アクション
    /// </summary>
    public abstract record Action
    {
        /// <summary>
        ///     アクションを検証し、結果を返す
        /// </summary>
        [NotNull]
        public abstract Result ValidateAbstract([NotNull] IActionContext context);
    }

    public abstract record Action<TResult> : Action where TResult : Result
    {
        public override Result ValidateAbstract(IActionContext context)
        {
            // Unity 2021.3 では `Covariant return types` をサポートしていないため、命名を同じにできない
            return Validate(context);
        }

        /// <summary>
        ///     アクションを検証し、結果を返す
        /// </summary>
        [NotNull]
        public abstract TResult Validate([NotNull] IActionContext context);
    }
}
