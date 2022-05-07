using UnityEngine;
using Views.Strategy.Cards;
using Views.Strategy.Units;

namespace Views.Strategy
{
    public class ActionSelector : MonoBehaviour
    {
        [Header("Components")] public StrategyWindow window;

        [Header("States")] [Space] public HandCardView hoveringHandCardView;
        public HandCardView selectingHandCardView;
        public UnitStatusView hoveringUnitStatusView;
        public UnitStatusView selectingUnitStatusView;
    }
}
