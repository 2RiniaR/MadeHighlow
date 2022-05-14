using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PayCard
{
    /// <summary>
    ///     カードを支払うアクションに対して、影響を与えるもの
    /// </summary>
    public interface IPayCardEffector : IComponent
    {
        /// <summary>
        ///     カードを支払うアクションに対して与える影響を返す
        /// </summary>
        public ValueList<Interrupt<PayCardEffect>> EffectsOnPayCard(
            [NotNull] IHistory history,
            [NotNull] Card target
        );
    }
}
