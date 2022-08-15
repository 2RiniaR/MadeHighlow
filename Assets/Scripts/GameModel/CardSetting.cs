using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    [CreateAssetMenu(fileName = "New Command Setting", menuName = "MADE HIGHLOW/Command Setting", order = 0)]
    public class CardSetting : ScriptableObject
    {
        public Sprite iconImage;
        public Sprite frameImage;
    }
}