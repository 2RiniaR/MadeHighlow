using General.Adapters;
using UnityEngine;

namespace Views.Strategy.Players
{
    public class PlayerStatusView : MonoBehaviour
    {
        [Header("Components")]
        public TextViewer nameText;
        public PlayerUnitCollection units;

        [Header("States"), Space]
        public int id;
        public string playerName;

        private void Update()
        {
            UpdateNameText();
        }

        private void UpdateNameText()
        {
            nameText.Content = playerName;
        }
    }
}