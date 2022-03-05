using System;
using UniRx;

namespace General.Components
{
    public interface IPulseTrigger
    {
        public IObservable<Unit> OnTriggeredObservable();
    }
}