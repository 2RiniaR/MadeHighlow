using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    public class Command : MonoBehaviour
    {
        public CommandQuickness quickness;
        public Figure figure;
        public List<Card> payCards;
        private readonly List<ICommandRunner> _runners = new();

        public void RegisterRunner(ICommandRunner runner)
        {
            _runners.Add(runner);
        }

        public async UniTask Run(CancellationToken token)
        {
            foreach (var runner in _runners)
            {
                await runner.Run(token);
                if (token.IsCancellationRequested) return;
            }
        }
    }
}