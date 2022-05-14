using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     地上
    /// </summary>
    public record GroundElevation([NotNull] Height Height, bool Placeable) : Elevation(Placeable);
}
