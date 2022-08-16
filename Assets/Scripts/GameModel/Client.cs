using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    public class Client : MonoBehaviour
    {
        public IStrategySelector StrategySelector { get; set; }
        public IEventPlayer EventPlayer { get; set; }

        public UniTask PlayEventsToLatest(CancellationToken token)
        {
            if (EventPlayer == null) return UniTask.CompletedTask;
            return EventPlayer.PlayEventsToLatest(token);
        }

        public UniTask SelectStrategy(Player submitter, CancellationToken token)
        {
            if (StrategySelector == null) return UniTask.CompletedTask;
            return StrategySelector.SelectStrategy(submitter, token);
        }
    }
}