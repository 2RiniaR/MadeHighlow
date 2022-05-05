using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     カードを支払うアクションに対して、影響を与えるもの
    /// </summary>
    public interface IPayCardEffector : IComponent
    {
        /// <summary>
        ///     カードを支払うアクションに対して与える影響を返す
        /// </summary>
        public PayCardEffect EffectOnPayCard([NotNull] in IActionContext context, [NotNull] in PayCardAction action);
    }
}