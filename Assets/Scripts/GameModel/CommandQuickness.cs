using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    [CreateAssetMenu(fileName = "New Command Quickness", menuName = "MADE HIGHLOW/Command Quickness", order = 0)]
    public class CommandQuickness : ScriptableObject
    {
        public int priority;
        public Sprite indicatorImage;
    }
}