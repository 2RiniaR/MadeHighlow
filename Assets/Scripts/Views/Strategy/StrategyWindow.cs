using UnityEngine;
using Views.Manual;
using Views.Menu;
using Views.RobotChat;
using Views.Strategy.Cards;
using Views.Strategy.Logs;
using Views.Strategy.Players;
using Views.Strategy.Units;

namespace Views.Strategy
{
    public class StrategyWindow : MonoBehaviour, IManualOpener, IMenuOpener, IRobotChatOpener
    {
        [Header("Components")] public UnitCollection units;

        public PlayerCollection players;
        public CardCollection cards;
        public LogCollection logs;
        public TimeLimit timeLimit;
        public TurnCount turnCount;
        public Instruction instruction;
        public OpenManualTrigger openManualTrigger;
        public OpenMenuTrigger openMenuTrigger;
        public OpenRobotChatTrigger openRobotChatTrigger;

        [Header("Prefabs")] [Space] public ManualWindow manualWindow;

        public MenuWindow menuWindow;
        public RobotChatWindow robotChatWindow;

        [Header("States")] [Space] public Transform child;

        private void Start()
        {
            openManualTrigger.Opener = this;
            openMenuTrigger.Opener = this;
            openRobotChatTrigger.Opener = this;
        }

        public void OpenManual()
        {
            if (child != null) return;

            var window = Instantiate(manualWindow, transform);
            child = window.transform;
        }

        public void OpenMenu()
        {
            if (child != null) return;

            var window = Instantiate(menuWindow, transform);
            child = window.transform;
        }

        public void OpenRobotChat()
        {
            if (child != null) return;

            var window = Instantiate(robotChatWindow, transform);
            child = window.transform;
        }
    }
}