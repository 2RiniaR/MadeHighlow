using UnityEngine;

namespace RineaR.MadeHighlow.Clients.Local.Strategy
{
    [CreateAssetMenu(fileName = "New Strategy Checker", menuName = "MADE HIGHLOW/Local Client/Strategy Checker",
        order = 0)]
    public class StrategyChecker : ScriptableObject
    {
        public bool IsValid()
        {
            return true;
        }
    }
}