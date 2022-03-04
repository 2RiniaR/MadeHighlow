using General.Components;
using General.Components.Adapters;
using UnityEngine;

namespace GameView.Strategy
{
    public class TurnCount : MonoBehaviour
    {
        [Header("Components")]
        [SerializeReference] public ITextView timerImage = new TMPTextView();

        [Header("States"), Space]
        [Min(0)] public int currentTurn;

        private void Update()
        {
            UpdateTurnView();
        }

        private void UpdateTurnView()
        {
            timerImage.Content = Mathf.Clamp(currentTurn, 0, 99).ToString();
        }
    }
}