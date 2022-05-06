using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ダメージを与えるアクションに対して、影響を与えるもの
    /// </summary>
    public interface IInstantDamageEffector : IComponent
    {
        public InstantDamageEffect EffectOnInstantDamage(
            [NotNull] in IActionContext context,
            [NotNull] in InstantDamageAction action
        );
    }
}