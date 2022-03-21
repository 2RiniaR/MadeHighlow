using UnityEngine;

namespace General.UI.ItemSelector
{
    public class SelectedItemReceiver : MonoBehaviour
    {
        public ClickItemSelector selecting;

        private void Update()
        {
            ResetIfUnavailable();
        }

        private void ResetIfUnavailable()
        {
            if (selecting == null)
            {
                return;
            }

            if (selecting.gameObject.CompareTag(gameObject.tag) == false)
            {
                selecting = null;
            }
        }
    }
}