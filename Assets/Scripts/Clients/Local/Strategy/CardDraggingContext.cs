using System;
using UniRx;
using UnityEngine;

namespace RineaR.MadeHighlow.Clients.Local.Strategy
{
    public class CardDraggingContext : MonoBehaviour
    {
        private readonly Subject<(CardDragger card, CardDropTarget target)> _onDropped = new();
        private readonly Subject<CardDragger> _onHovered = new();

        public CardDragger Dragging { get; private set; }
        public IObservable<CardDragger> OnHovered => _onHovered;
        public IObservable<(CardDragger card, CardDropTarget target)> OnDropped => _onDropped;

        public void PublishCardHovered(CardDragger cardDragger)
        {
            Dragging = cardDragger;
            _onHovered.OnNext(cardDragger);
        }

        public void PublishCardDropped(CardDragger cardDragger, CardDropTarget target)
        {
            Dragging = null;
            _onDropped.OnNext((cardDragger, target));
        }
    }
}