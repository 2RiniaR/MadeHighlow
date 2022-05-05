using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     アクション
    /// </summary>
    public interface IValidatable
    {
        /// <summary>
        ///     空のアクション
        /// </summary>
        public IValidatable Empty => new EmptyImpl();

        [NotNull]
        public ISimulatable Validate([NotNull] in IActionContext context);

        private record EmptyImpl : IValidatable
        {
            public ISimulatable Validate(in IActionContext context)
            {
                return ISimulatable.Empty;
            }
        }
    }
}