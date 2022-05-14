using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace General.UI.ItemSelector
{
    public class ClickItemSelector : MonoBehaviour, IPointerClickHandler
    {
        private SelectedItemReceiver _receiver;
        public bool IsSelecting { get; private set; }

        private void Update()
        {
            UpdateReceiver();
            UpdateSelectingState();
        }

        private void UpdateReceiver()
        {
            if (IsReceiverAvailable())
            {
                return;
            }

            FindReceiver();
        }

        private void UpdateSelectingState()
        {
            IsSelecting = _receiver != null && _receiver.selecting == this;
        }

        private void FindReceiver()
        {
            var receivers = FindObjectsOfType<SelectedItemReceiver>().ToList();
            _receiver = receivers.Find(receiver => receiver.gameObject.CompareTag(gameObject.tag));
        }

        private bool IsReceiverAvailable()
        {
            return _receiver != null && _receiver.gameObject.CompareTag(gameObject.tag);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_receiver == null)
            {
                return;
            }

            _receiver.selecting = this;
        }
    }
}
