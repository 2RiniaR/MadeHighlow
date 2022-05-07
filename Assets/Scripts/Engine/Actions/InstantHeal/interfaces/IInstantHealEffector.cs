using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     治癒効果を与えるアクションに対して、影響を与えるもの
    /// </summary>
    public interface IInstantHealEffector : IComponent
    {
        public InstantHealEffect EffectOnInstantHeal(
            [NotNull] IActionContext context,
            [NotNull] InstantHealAction action
        );
    }
}
