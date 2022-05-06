using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     地上
    /// </summary>
    public record GroundElevation([NotNull] in Height Height, in bool Placeable) : Elevation(Placeable);
}