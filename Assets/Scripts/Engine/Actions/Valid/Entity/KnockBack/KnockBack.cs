using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.KnockBack
{
    public record KnockBack([NotNull] Direction3D Direction, [NotNull] Distance Distance);
}
