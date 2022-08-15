using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    [CreateAssetMenu(fileName = "New Card Genre", menuName = "MADE HIGHLOW/Card Genre", order = 0)]
    public class CardGenre : ScriptableObject
    {
        public Sprite baseImage;
        public Sprite shadowImage;
    }
}