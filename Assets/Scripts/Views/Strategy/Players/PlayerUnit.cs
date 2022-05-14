using General.Adapters;
using UnityEngine;

namespace Views.Strategy.Players
{
    public class PlayerUnit : MonoBehaviour
    {
        [Header("Components")] public SpriteViewer characterIconImage;
        public TextViewer healthText;

        [Header("States")] [Space] public int id;
        public Sprite icon;
        public int health;

        private void Update()
        {
            UpdateCharacterIcon();
            UpdateHealthView();
        }

        private void UpdateCharacterIcon()
        {
            characterIconImage.Sprite = icon;
        }

        private void UpdateHealthView()
        {
            healthText.Content = health.ToString();
        }
    }
}
