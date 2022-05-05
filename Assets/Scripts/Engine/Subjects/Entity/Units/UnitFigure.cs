using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     「ユニット」の性格
    /// </summary>
    public abstract record UnitFigure
    {
        [NotNull] public static UnitFigure Empty => new EmptyUnitFigure();

        private record EmptyUnitFigure : UnitFigure;
    }
}