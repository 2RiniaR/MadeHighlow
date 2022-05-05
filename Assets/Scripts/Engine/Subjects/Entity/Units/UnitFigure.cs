using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     「ユニット」の性格
    /// </summary>
    public abstract record UnitFigure
    {
        [NotNull] public static UnitFigure Empty => new EmptyImpl();

        private record EmptyImpl : UnitFigure;
    }
}