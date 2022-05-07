using General.Adapters;
using UnityEngine;

namespace Views.Strategy
{
    public class TurnCount : MonoBehaviour
    {
        [Header("Components")] public TextViewer timerImage;

        [Header("States")] [Space] [Min(0)] public int currentTurn;

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
