using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Components.UnitEffects
{
    /// <summary>
    ///     「カウンター」
    /// </summary>
    /// <remarks>
    ///     自身がいるマスを背後以外から通過したユニットに対して、攻撃を行う。
    /// </remarks>
    public record Counter(in ID ID, [NotNull] in IAttachableID AttachedID, [NotNull] in Duration Duration) : Component(
        in ID,
        in AttachedID,
        in Duration
    ), IStepInReactor
    {
        public ValueObjectList<StepInReaction> OnSteppedIn(in IActionContext session, in EntityID actor)
        {
            throw new NotImplementedException();
        }
    }
}