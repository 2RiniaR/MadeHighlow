using General.Components;
using General.Components.Adapters;
using UniRx;
using UnityEngine;

namespace GameView.Strategy
{
    public class OpenManualTrigger : MonoBehaviour
    {
        [Header("Components")] [SerializeReference]
        private IPulseTrigger _trigger = new ButtonClickPulseTrigger();

        public IManualOpener Opener { get; set; }

        private void Start()
        {
            _trigger.OnTriggeredObservable().Subscribe(_ => OpenManual()).AddTo(this);
        }

        private void OpenManual()
        {

        }
    }
}