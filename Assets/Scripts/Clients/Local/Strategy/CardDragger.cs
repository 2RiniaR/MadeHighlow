using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RineaR.MadeHighlow.Clients.Local.Strategy
{
    [RequireComponent(typeof(CardView))]
    public class CardDragger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public CardView view;
        public CardDraggingContext context;
        private Vector3 _startDragPosition;

        private void Reset()
        {
            RefreshReferences();
        }

        private void Start()
        {
            RefreshReferences();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _startDragPosition = transform.position;
            context.PublishCardHovered(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            FollowPointer(eventData.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            var target = DecideTarget(eventData);
            context.PublishCardDropped(this, target);
            transform.position = _startDragPosition;
        }

        private void RefreshReferences()
        {
            view = GetComponent<CardView>() ?? throw new NullReferenceException();
            context ??= GetComponentInParent<CardDraggingContext>();
        }

        private void FollowPointer(Vector2 pointerPosition)
        {
            if (Camera.main == null)
            {
                return;
            }

            var targetPosition = new Vector3(pointerPosition.x, pointerPosition.y, -1);
            transform.position = targetPosition;
        }

        private CardDropTarget DecideTarget(PointerEventData eventData)
        {
            var raycastHits = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycastHits);

            var minDepth = int.MaxValue;
            CardDropTarget target = null;
            foreach (var hit in raycastHits)
            {
                if (minDepth <= hit.depth)
                {
                    continue;
                }

                var component = hit.gameObject.GetComponent<CardDropTarget>();
                if (component == null)
                {
                    continue;
                }

                target = component;
            }

            return target;
        }
    }
}