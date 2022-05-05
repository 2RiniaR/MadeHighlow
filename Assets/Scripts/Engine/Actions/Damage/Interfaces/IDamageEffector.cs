using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ダメージを与えるアクションに対して、影響を与えるもの
    /// </summary>
    public interface IDamageEffector : IComponent
    {
        public DamageEffect EffectOnDamage([NotNull] in IActionContext session, [NotNull] in DamageAction action);
    }
}