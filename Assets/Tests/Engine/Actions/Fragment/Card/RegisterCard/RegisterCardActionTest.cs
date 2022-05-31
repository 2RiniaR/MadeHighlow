using NUnit.Framework;

namespace RineaR.MadeHighlow.Actions.RegisterCard
{
    public class RegisterCardActionTest
    {
        /// <summary>
        ///     正常な場合、「成功」の結果が返ってくる
        /// </summary>
        [Test]
        public void Evaluate_Valid_ReturnsSucceed()
        {
        }

        /// <summary>
        ///     登録先のプレイヤーが存在しない場合、「失敗」の結果が返ってくる
        /// </summary>
        [Test]
        public void Evaluate_NoPlayer_ReturnsFailed()
        {
        }
    }
}
