using General.Adapters;
using UnityEngine;

namespace Views.Strategy.Units
{
    public class UnitStatusView : MonoBehaviour
    {
        [Header("Components")]
        public SpriteViewer characterVisualImage;
        public SpriteViewer characterIconImage;
        public TextViewer nameText;
        public TextViewer healthText;
        public TextViewer strengthText;
        public BooleanViewer cardAvailableExpression;
        public UnitHealthHearts healthHearts;
        public UnitEffectIconCollection effectIcons;

        [Header("States"), Space]
        public int id;
        public string displayName;
        public int strength;
        public Sprite visual;
        public Sprite icon;
        public bool isCardAvailable;

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
            UpdateCardAvailableView();
        }

        private void UpdateObjectName()
        {
            gameObject.name = $"{displayName}";
        }

        private void UpdateCharacterView()
        {
            nameText.Content = displayName;
            characterVisualImage.Sprite = visual;
            characterIconImage.Sprite = icon;
        }

        private void UpdateHealthText()
        {
            healthText.Content = Health.ToString();
        }

        private void UpdateStrengthText()
        {
            strengthText.Content = Strength.ToString();
        }

        private void UpdateCardAvailableView()
        {
            cardAvailableExpression.Value = isCardAvailable;
        }

        public void ShowDetail()
        {

        }

        public void Submit()
        {

        }
    }
}