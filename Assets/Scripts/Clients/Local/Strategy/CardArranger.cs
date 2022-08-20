using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using RineaR.MadeHighlow.Clients.Local.Strategy.Tools;
using RineaR.MadeHighlow.GameData.CardEffects;
using RineaR.MadeHighlow.GameModel;
using UnityEngine;

namespace RineaR.MadeHighlow.Clients.Local.Strategy
{
    public class CardArranger : MonoBehaviour
    {
        [Header("Settings")]
        public WalkRouteSelector walkRouteSelector;

        private async UniTask<WalkRoute> SelectWalkRoute(Figure walker, CancellationToken token)
        {
            var parentWindow = Window.ContainerOf(this);
            var window = parentWindow.CreateChild(walkRouteSelector.window);
            window.Open();

            var selector = window.GetComponent<WalkRouteSelector>() ?? throw new NullReferenceException();
            var route = await selector.SelectWalkRoute(walker, token);

            window.Close();
            window.Dispose();
            return route;
        }

        public async UniTask Arrange(Card card, Figure target, CancellationToken token)
        {
            var activators = card.GetActivators();
            foreach (var activator in activators)
            {
                await ArrangeActivation(activator, target, token);
            }
        }

        private async UniTask ArrangeActivation(Component activator, Figure target, CancellationToken token)
        {
            if (activator is WalkActivator walk)
            {
                var route = await SelectWalkRoute(target, token);
                if (token.IsCancellationRequested)
                {
                    return;
                }

                var runner = walk.Activate(target, route);
                target.session.commandStack.Push(runner.command);
            }
        }
    }
}