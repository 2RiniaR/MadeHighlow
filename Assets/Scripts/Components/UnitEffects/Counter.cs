using System;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions;
using RineaR.MadeHighlow.Actions.Valid;

namespace RineaR.MadeHighlow.Components.UnitEffects
{
    /// <summary>
    ///     「カウンター」
    /// </summary>
    /// <remarks>
    ///     自身がいるマスを背後以外から通過したユニットに対して、攻撃を行う。
    /// </remarks>
    public record Counter(ID ID, [NotNull] IAttachableID AttachedID, [NotNull] Duration Duration) : Component(
        ID,
        AttachedID,
        Duration
    ), IStepInReactor
    {
        public ValueList<StepInReaction> OnSteppedIn(IHistory session, EntityID actor)
        {
            throw new NotImplementedException();
        }
    }
}
