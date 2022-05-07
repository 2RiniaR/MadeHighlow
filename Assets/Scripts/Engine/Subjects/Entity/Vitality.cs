using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     エンティティの生存状態
    /// </summary>
    public sealed record Vitality([NotNull] Health Health, [NotNull] Health MaxHealth)
    {
        public bool IsDead => Health.Value <= MaxHealth.Value;

        public Vitality Dead => this with { Health = new Health(0) };
    }
}
