using RineaR.MadeHighlow.Engine.Subjects.Objects.Components;

namespace RineaR.MadeHighlow.Components.UnitEffects
{
    /// <summary>
    ///     「俊敏化」
    /// </summary>
    public record Haste() : Component(new ComponentType(nameof(Guard)));
}