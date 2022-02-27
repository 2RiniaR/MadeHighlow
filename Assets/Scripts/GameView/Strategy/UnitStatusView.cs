using GameView.Structures;
using General.Components;
using General.Components.Adapters;
using UnityEngine;

namespace GameView.Strategy
{
    public class UnitStatusView : MonoBehaviour
    {
        [Header("Components")]
        [SerializeReference] public ISpriteView MainImage = new UIImageSpriteView();
        [SerializeReference] public ISpriteView IconImage = new UIImageSpriteView();
        [SerializeReference] public ITextView NameText = new TMPTextView();
        [SerializeReference] public ITextView HealthText = new TMPTextView();
        [SerializeReference] public ITextView StrengthText = new TMPTextView();
        [SerializeField] public UnitHealthHearts healthHearts;

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
            UpdateCharacterView();
            UpdateHealthText();
            UpdateStrengthText();
        }

        private void UpdateCharacterView()
        {
            NameText.Content = Character.name;
            MainImage.Image = Character.mainImage;
            IconImage.Image = Character.iconImage;
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