using General.Adapters;
using UniRx;
using UnityEngine;

namespace Views.Strategy
{
    public class OpenMenuTrigger : MonoBehaviour
    {
        [SerializeField, Header("Components")]
        private PulseTrigger trigger;

        public IMenuOpener Opener { get; set; }

        private void Start()
        {
            trigger.OnTriggeredObservable().Subscribe(_ => OpenMenu()).AddTo(this);
        }

        private void OpenMenu()
        {
            Opener.OpenMenu();
        }
    }
}