using General.Adapters;
using UniRx;
using UnityEngine;

namespace Views.Strategy
{
    public class OpenManualTrigger : MonoBehaviour
    {
        [SerializeField] [Header("Components")]
        private PulseTrigger trigger;

        public IManualOpener Opener { get; set; }

        private void Start()
        {
            trigger.OnTriggeredObservable().Subscribe(_ => OpenManual()).AddTo(this);
        }

        private void OpenManual()
        {
            Opener.OpenManual();
        }
    }
}
