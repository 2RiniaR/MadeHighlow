using GameView.Structures;
using General.Components;
using General.Components.Adapters;
using UnityEngine;

namespace GameView.Strategy.Units
{
    public class UnitStatus : MonoBehaviour
    {
        [Header("Components")]
        [SerializeReference] public ISpriteView CharacterVisualImage = new UIImageSpriteView();
        [SerializeReference] public ISpriteView CharacterIconImage = new UIImageSpriteView();
        [SerializeReference] public ITextView NameText = new TMPTextView();
        [SerializeReference] public ITextView HealthText = new TMPTextView();
        [SerializeReference] public ITextView StrengthText = new TMPTextView();
        public UnitHealthHearts healthHearts;
        public UnitEffectIconCollection effectIcons;

        [Header("States"), Space]
        public CharacterData character;
        public int strength;

        public CharacterData Character
        {
            get => character;
            set => character = value;
        }

        public int InitialHealth
        {
            get => healthHearts.initialHealth;
            set => healthHearts.initialHealth = value;
        }

        public int Health
        {
            get => healthHearts.currentHealth;
            set => healthHearts.currentHealth = value;
        }

        public int Strength
        {
            get => strength;
            set => strength = value;
        }

        private void Update()
        {
            UpdateObjectName();
            UpdateCharacterView();
            UpdateHealthText();
            UpdateStrengthText();
        }

        private void UpdateObjectName()
        {
            gameObject.name = $"{character.displayName}";
        }

        private void UpdateCharacterView()
        {
            NameText.Content = Character.displayName;
            CharacterVisualImage.Image = Character.visualImage;
            CharacterIconImage.Image = Character.iconImage;
        }

        private void UpdateHealthText()
        {
            HealthText.Content = Health.ToString();
        }

        private void UpdateStrengthText()
        {
            StrengthText.Content = Strength.ToString();
        }
    }
}