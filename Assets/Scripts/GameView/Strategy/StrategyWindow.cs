using GameView.Strategy.Cards;
using GameView.Strategy.Logs;
using GameView.Strategy.Players;
using GameView.Strategy.Units;
using UnityEngine;

namespace GameView.Strategy
{
    public class StrategyWindow : MonoBehaviour, IManualOpener, IMenuOpener, IRobotChatOpener
    {
        [Header("Components")]
        public UnitStatusCollection units;
        public PlayerStatusCollection players;
        public CardCollection cards;
        public LogCollection logs;
        public TimeLimit timeLimit;
        public TurnCount turnCount;
        public Instruction instruction;
        public OpenManualTrigger openManualTrigger;
        public OpenMenuTrigger openMenuTrigger;
        public OpenRobotChatTrigger openRobotChatTrigger;

        private void Start()
        {
            openManualTrigger.Opener = this;
            openMenuTrigger.Opener = this;
            openRobotChatTrigger.Opener = this;
        }

        public void OpenManual()
        {

        }

        public void OpenMenu()
        {

        }

        public void OpenRobotChat()
        {

        }
    }
}
