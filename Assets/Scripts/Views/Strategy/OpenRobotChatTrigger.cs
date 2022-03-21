using General.Adapters;
using UniRx;
using UnityEngine;

namespace Views.Strategy
{
    public class OpenRobotChatTrigger : MonoBehaviour
    {
        [SerializeField, Header("Components")]
        private PulseTrigger trigger;

        public IRobotChatOpener Opener { get; set; }

        private void Start()
        {
            trigger.OnTriggeredObservable().Subscribe(_ => OpenRobotChat()).AddTo(this);
        }

        private void OpenRobotChat()
        {
            Opener.OpenRobotChat();
        }
    }
}