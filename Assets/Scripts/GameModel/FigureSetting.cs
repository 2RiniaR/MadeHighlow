using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    [CreateAssetMenu(fileName = "New Figure Setting", menuName = "MADE HIGHLOW/Figure Setting", order = 0)]
    public class FigureSetting : ScriptableObject
    {
        public Sprite faceImage;
        public Sprite iconImage;
        public string displayName;
    }
}