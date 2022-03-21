using General.Adapters;
using UnityEngine;

namespace Views.Strategy.Units
{
    public class UnitEffectIcon : MonoBehaviour
    {
        [Header("Components")]
        public SpriteViewer iconImage;

        [Header("States"), Space]
        public int id;
        public string displayName;
        public Sprite icon;
        public int duration;

        private void Update()
        {
            UpdateObjectName();
            UpdateIconImage();
        }

        private void UpdateObjectName()
        {
            gameObject.name = $"{displayName}";
        }

        private void UpdateIconImage()
        {
            iconImage.Sprite = icon;
        }
    }
}