using GameView.Structures;
using General.Components;
using General.Components.Adapters;
using UnityEngine;

namespace GameView.Strategy.Cards
{
    public class Card : MonoBehaviour
    {
        [Header("Components")]
        [SerializeReference] public ISpriteView CardImage = new UIImageSpriteView();

        [Header("States"), Space]
        public CardData card;

        private void Update()
        {
            UpdateCardView();
        }

        private void UpdateCardView()
        {
            CardImage.Image = card.image;
        }
    }
}