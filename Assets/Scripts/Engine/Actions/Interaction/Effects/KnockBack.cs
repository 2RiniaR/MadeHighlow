using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Subjects.Geometry;

namespace RineaR.MadeHighlow.Engine.Actions.Interaction.Effects
{
    public record KnockBack : InteractionEffect
    {
        private KnockBack(KnockBackReferenceMethod referenceMethod) : base(InteractionEffectType.KnockBack)
        {
            ReferenceMethod = referenceMethod;
        }

        /// <summary>
        ///     参照方法
        /// </summary>
        public KnockBackReferenceMethod ReferenceMethod { get; }

        /// <summary>
        ///     定数の「ノックバック」
        /// </summary>
        /// <param name="Distance">距離</param>
        public record FromConstant
        (
            [NotNull] Distance Distance
        ) : KnockBack(KnockBackReferenceMethod.FromConstant);
    }
}