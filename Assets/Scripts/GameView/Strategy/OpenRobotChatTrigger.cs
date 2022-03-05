using General.Components;
using General.Components.Adapters;
using UniRx;
using UnityEngine;

namespace GameView.Strategy
{
    public class OpenRobotChatTrigger : MonoBehaviour
    {
        [Header("Components")] [SerializeReference]
        private IPulseTrigger _trigger = new ButtonClickPulseTrigger();

        public IRobotChatOpener Opener { get; set; }

        private void Start()
        {
            _trigger.OnTriggeredObservable().Subscribe(_ => OpenRobotChat()).AddTo(this);
        }

        private void OpenRobotChat()
        {

        }
    }
}