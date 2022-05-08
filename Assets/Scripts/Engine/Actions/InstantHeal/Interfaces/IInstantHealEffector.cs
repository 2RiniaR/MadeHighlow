using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     治癒を与えるアクションに対して、影響を与えるもの
    /// </summary>
    public interface IInstantHealEffector
    {
        public ValueList<Interrupt<InstantHealEffect>> EffectsOnInstantHeal(
            [NotNull] IActionContext context,
            [NotNull] InstantHealAction action
        );
    }
}
