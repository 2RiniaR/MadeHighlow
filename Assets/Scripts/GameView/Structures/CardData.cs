using UnityEngine;

namespace GameView.Structures
{
    [CreateAssetMenu(fileName = "New Card", menuName = "MadeHighlow/Card")]
    public class CardData : ScriptableObject
    {
        public Sprite image;
        public string displayName;
    }
}