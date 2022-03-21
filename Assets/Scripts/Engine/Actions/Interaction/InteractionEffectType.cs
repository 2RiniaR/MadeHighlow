namespace RineaR.MadeHighlow.Engine.Actions.Interaction
{
    /// <summary>
    ///     「攻撃の効果」の種類
    /// </summary>
    public enum InteractionEffectType
    {
        Damage,

        /// <summary>
        ///     ノックバック
        /// </summary>
        KnockBack,

        /// <summary>
        ///     即死
        /// </summary>
        Death,

        /// <summary>
        ///     エフェクト付与
        /// </summary>
        EffectGiving,
    }
}