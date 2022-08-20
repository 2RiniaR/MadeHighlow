using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace RineaR.MadeHighlow.Clients.Local.Strategy
{
    public class CardDropTarget : MonoBehaviour
    {
        public Image areaIndicatorImage;
        public bool droppable = true;
        public CardDraggingContext context;
        private readonly Subject<CardDragger> _onDropped = new();
        private bool _draggingExist;
        public bool Opening => _draggingExist && droppable;

        private void Reset()
        {
            RefreshReferences();
        }

        private void Start()
        {
            RefreshReferences();
            context.OnHovered.Subscribe(OnCardHovered).AddTo(this);
            context.OnDropped.Subscribe(x => OnCardDropped(x.card, x.target)).AddTo(this);
        }

        private void Update()
        {
            SetAnimationState();
        }

        private void OnDestroy()
        {
            _onDropped.Dispose();
        }

        private void RefreshReferences()
        {
            context ??= GetComponentInParent<CardDraggingContext>();
        }

        public void OnCardHovered(CardDragger cardDragger)
        {
            _draggingExist = true;
        }

        public void OnCardDropped(CardDragger cardDragger, CardDropTarget target)
        {
            if (target == this && Opening)
            {
                _onDropped.OnNext(cardDragger);
            }

            _draggingExist = false;
        }

        private void SetAnimationState()
        {
            areaIndicatorImage.enabled = Opening;
        }

        public IObservable<CardDragger> OnDropAsObservable()
        {
            return _onDropped;
        }
    }
}