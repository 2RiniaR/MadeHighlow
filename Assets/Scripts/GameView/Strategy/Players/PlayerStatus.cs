using General.Components;
using General.Components.Adapters;
using UnityEngine;

namespace GameView.Strategy.Players
{
    public class PlayerStatus : MonoBehaviour
    {
        [Header("Components")]
        [SerializeReference] public ITextView NameText = new TMPTextView();
        public PlayerUnitCollection units;

        [Header("States"), Space]
        public string playerName;

        private void Update()
        {
            UpdateNameText();
        }

        private void UpdateNameText()
        {
            NameText.Content = playerName;
        }
    }
}