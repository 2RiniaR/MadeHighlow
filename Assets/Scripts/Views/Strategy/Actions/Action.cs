using General.Adapters;
using UnityEngine;

namespace Views.Strategy.Actions
{
    public class Action : MonoBehaviour
    {
        [Header("Components")] public SpriteViewer cardImage;

        [Header("States")] [Space] public int id;
        // public Card card;

        private void Update()
        {
            UpdateCardView();
        }

        private void UpdateCardView()
        {
            // cardImage.Sprite = card.image;
        }

        public void ShowDetail()
        {
        }
    }
}