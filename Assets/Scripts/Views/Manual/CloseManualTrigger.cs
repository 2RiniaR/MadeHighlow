using General.Adapters;
using UniRx;
using UnityEngine;

namespace Views.Manual
{
    public class CloseManualTrigger : MonoBehaviour
    {
        [SerializeField, Header("Components")]
        private PulseTrigger trigger;

        public IManualCloser Closer { get; set; }

        private void Start()
        {
            trigger.OnTriggeredObservable().Subscribe(_ => CloseManual()).AddTo(this);
        }

        private void CloseManual()
        {
            Closer.CloseManual();
        }
    }
}