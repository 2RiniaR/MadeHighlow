using General.Adapters;
using UnityEngine;

namespace Views.Strategy.Cards
{
    public class HandCardView : MonoBehaviour
    {
        [Header("Components")]
        public SpriteViewer cardImage;

        [Header("States"), Space]
        public int id;
        public Sprite image;

        private void Update()
        {
            UpdateCardView();
        }

        private void UpdateCardView()
        {
            cardImage.Sprite = image;
        }
    }
}