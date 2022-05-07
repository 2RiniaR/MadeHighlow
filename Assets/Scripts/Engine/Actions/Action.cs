using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     アクション
    /// </summary>
    public abstract record Action
    {
        /// <summary>
        ///     中身がないアクション
        /// </summary>
        public static Action Empty => new EmptyImpl();

        /// <summary>
        ///     アクションを検証し、結果を返す
        /// </summary>
        [NotNull]
        public abstract Result ValidateAbstract([NotNull] IActionContext context);

        private record EmptyImpl : Action
        {
            public override Result ValidateAbstract(IActionContext context)
            {
                return Result.Empty;
            }
        }
    }

    /// <summary>
    ///     アクション
    /// </summary>
    public abstract record Action<TResult> : Action where TResult : Result
    {
        public override Result ValidateAbstract(IActionContext context)
        {
            // Unity 2021.2 では「overrideされたメソッドの戻り値の共変性」をサポートしていないため、命名を同じにできない
            return Validate(context);
        }

        /// <summary>
        ///     アクションを検証し、結果を返す
        /// </summary>
        [NotNull]
        public abstract TResult Validate([NotNull] IActionContext context);
    }
}