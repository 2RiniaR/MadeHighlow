using General.Components;
using General.Components.Adapters;
using UniRx;
using UnityEngine;

namespace GameView.Strategy
{
    public class OpenMenuTrigger : MonoBehaviour
    {
        [Header("Components")] [SerializeReference]
        private IPulseTrigger _trigger = new ButtonClickPulseTrigger();

        public IMenuOpener Opener { get; set; }

        private void Start()
        {
            _trigger.OnTriggeredObservable().Subscribe(_ => OpenMenu()).AddTo(this);
        }

        private void OpenMenu()
        {

        }
    }
}