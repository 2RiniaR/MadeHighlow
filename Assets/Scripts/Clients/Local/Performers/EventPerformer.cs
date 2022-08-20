using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using RineaR.MadeHighlow.Clients.Local.Actors;
using RineaR.MadeHighlow.GameData.Commands;
using RineaR.MadeHighlow.GameModel;
using RineaR.MadeHighlow.GameModel.Events;
using UnityEngine;

namespace RineaR.MadeHighlow.Clients.Local.Performers
{
    [RequireComponent(typeof(Window))]
    public class EventPerformer : MonoBehaviour
    {
        [Header("Requirements")]
        public Window window;

        [Header("References on scene")]
        public EventLogger logger;

        [Header("States")]
        public int completedEventIndex;

        private void Reset()
        {
            RefreshReferences();
        }

        private void Start()
        {
            RefreshReferences();
            completedEventIndex = 0;
        }

        private void RefreshReferences()
        {
            window = GetComponent<Window>() ?? throw new NullReferenceException();
        }

        /// <summary>
        ///     イベントの演出を再生し、完了するまで待つ。
        /// </summary>
        public async UniTask PerformToLatest(CancellationToken token)
        {
            while (completedEventIndex < logger.EventLogs.Count)
            {
                await Play(logger.EventLogs[completedEventIndex], token);
                if (token.IsCancellationRequested)
                {
                    return;
                }

                completedEventIndex += 1;
            }
        }

        private async UniTask Play(IEventLog eventLog, CancellationToken token)
        {
            if (eventLog is RunCommandLog runCommandLog)
            {
                if (runCommandLog.CommandResult is WalkResult walkResult)
                {
                    var actor = walkResult.Walker.GetComponent<FigureActor>();
                    if (actor == null)
                    {
                        return;
                    }

                    foreach (var direction in walkResult.Route.Directions)
                    {
                        await actor.Step(direction, token);
                        if (token.IsCancellationRequested)
                        {
                            return;
                        }
                    }
                }
            }
        }
    }
}