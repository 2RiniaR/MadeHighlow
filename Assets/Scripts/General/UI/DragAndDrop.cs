using UnityEngine;
using UnityEngine.EventSystems;

namespace General.UI
{
    public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Transform _transform;

        public Vector2 delta;
        public bool dragging;
        public PointerEventData.InputButton input;

        private void Start()
        {
            _transform = transform;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (eventData.button != input || dragging)
            {
                return;
            }

            var origin = _transform.position;
            delta = new Vector2(origin.x, origin.y) - eventData.position;
            dragging = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!dragging)
            {
                return;
            }

            MoveToPointer(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!dragging)
            {
                return;
            }

            MoveToPointer(eventData);
            delta = Vector2.zero;
            dragging = false;
        }

        private void MoveToPointer(PointerEventData eventData)
        {
            var targetPosition = eventData.position + delta;
            _transform.position = new Vector3(targetPosition.x, targetPosition.y, _transform.position.z);
        }
    }
}
