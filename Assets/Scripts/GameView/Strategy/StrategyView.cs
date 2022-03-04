using GameView.Strategy.Players;
using GameView.Strategy.Units;
using UnityEngine;

namespace GameView.Strategy
{
    public class StrategyView : MonoBehaviour
    {
        [Header("Components")]
        public UnitStatusCollection units;
        public PlayerStatusCollection players;
    }
}
