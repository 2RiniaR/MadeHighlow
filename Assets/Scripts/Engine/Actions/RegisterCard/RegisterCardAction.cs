using System;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     プレイヤーを新規登録するアクション
    /// </summary>
    public record RegisterCardAction : Action<RegisterCardResult>
    {
        public override RegisterCardResult Validate(in IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}