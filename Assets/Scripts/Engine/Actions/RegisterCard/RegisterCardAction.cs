using System;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     カードを新規登録するアクション
    /// </summary>
    public record RegisterCardAction : Action<RegisterCardResult>
    {
        public override RegisterCardResult Validate(IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}