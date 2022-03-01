using GameView.General;
using GameView.Strategy.Units;
using UnityEngine;

namespace GameView.Strategy
{
    public class StrategyView : MonoBehaviour
    {
        [Header("Components")]
        public CollectionMonoBehaviour<UnitStatus> units;
    }
}
