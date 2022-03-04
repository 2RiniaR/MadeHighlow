using GameView.Structures;
using General.Components;
using General.Components.Adapters;
using UnityEngine;

namespace GameView.Strategy.Players
{
    public class PlayerUnit : MonoBehaviour
    {
        [Header("Components")]
        [SerializeReference] public ISpriteView CharacterIconImage = new UIImageSpriteView();
        [SerializeReference] public ITextView HealthText = new TMPTextView();

        [Header("States"), Space]
        public CharacterData character;
        public int health;

        private void Update()
        {
            UpdateCharacterIcon();
            UpdateHealthView();
        }

        private void UpdateCharacterIcon()
        {
            CharacterIconImage.Image = character.iconImage;
        }

        private void UpdateHealthView()
        {
            HealthText.Content = health.ToString();
        }
    }
}