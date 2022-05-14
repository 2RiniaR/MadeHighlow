using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Directors.Environments;
using Environment = RineaR.MadeHighlow.Directors.Environments.Environment;

namespace RineaR.MadeHighlow.Directors
{
    /// <summary>
    ///     ゲームの一連の流れを進行する
    /// </summary>
    public class Director
    {
        public Director([NotNull] Environment environment)
        {
            Environment = environment;
            ClientRegistry = new ClientRegistry();
        }

        [NotNull] public Environment Environment { get; }
        [NotNull] public ClientRegistry ClientRegistry { get; }

        /// <summary>
        ///     ゲームを終了まで実行する
        /// </summary>
        public async UniTask<GameResult> Run(CancellationToken cancellationToken)
        {
            SupplyInitialCards();

            while (true)
            {
                await CommandByPlayers(cancellationToken);

                ApplyPersonality();
                ObserveUnitsSublimation();
            }
        }

        /// <summary>
        ///     プレイヤーのデッキにカードを配る
        /// </summary>
        private void SupplyInitialCards()
        {
        }

        private void ApplyPersonality()
        {
        }

        private void ObserveUnitsSublimation()
        {
        }

        private UniTask CommandByPlayers(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
