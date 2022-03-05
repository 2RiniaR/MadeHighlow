using System;
using UniRx;
using UnityEngine.UI;

namespace General.Components.Adapters
{
    [Serializable]
    public class ButtonClickPulseTrigger : IPulseTrigger
    {
        public Button button;

        public IObservable<Unit> OnTriggeredObservable()
        {
            return button.OnClickAsObservable();
        }
    }
}