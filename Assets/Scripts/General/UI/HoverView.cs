using UnityEngine;
using UnityEngine.EventSystems;

namespace General.UI
{
    public class HoverView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Components")] public GameObject detail;

        [Header("States")] [Space] public bool isDetailOpening;
        public bool enableHover;

        private void Update()
        {
            UpdateDetailVisibility();
        }

        private void UpdateDetailVisibility()
        {
            if (detail == null)
            {
                return;
            }

            detail.SetActive(isDetailOpening);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (enableHover == false)
            {
                return;
            }

            isDetailOpening = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (enableHover == false)
            {
                return;
            }

            isDetailOpening = false;
        }
    }
}
