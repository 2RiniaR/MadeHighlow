using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.KnockBack
{
    public record KnockBackAction(ID SourceID, [NotNull] EntityID TargetID, [NotNull] KnockBack KnockBack) : ValidAction<KnockBackResult>
    {
        protected override KnockBackResult EvaluateBody(IHistory history)
        {
            throw new NotImplementedException();
        }
    }
}
