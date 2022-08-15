using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using RineaR.MadeHighlow.Clients.Local.Field;
using RineaR.MadeHighlow.GameData.CardEffects;
using RineaR.MadeHighlow.GameModel;
using UnityEngine;

namespace RineaR.MadeHighlow.Clients.Local.Strategy
{
    [CreateAssetMenu(fileName = "New Card Arranger", menuName = "MADE HIGHLOW/Local Client/Card Arranger", order = 0)]
    public class CardArranger : ScriptableObject
    {
        public WalkRouteSelector walkRouteSelector;

        private async UniTask<WalkRoute> SelectWalkRoute(Figure walker, CancellationToken token)
        {
            if (walkRouteSelector == null)
            {
                Debug.LogWarning("Walk Route Selector is not exist.");
                return null;
            }

            var window = Window.Current.OpenAsChild(walkRouteSelector.window);
            var selector = window.GetComponent<WalkRouteSelector>() ?? throw new NullReferenceException();
            var route = await selector.SelectWalkRoute(walker, token);
            return route;
        }

        public async UniTask Arrange(Card card, Figure target, CancellationToken token)
        {
            var activators = card.GetActivators();
            foreach (var activator in activators) await ArrangeActivation(activator, target, token);
        }

        private async UniTask ArrangeActivation(Component activator, Figure target, CancellationToken token)
        {
            if (activator is WalkActivator walk)
            {
                var route = await SelectWalkRoute(target, token);
                if (token.IsCancellationRequested) return;
                var runner = walk.Activate(route);
                target.Session.CommandStack.Push(runner.command);
            }
        }
    }
}