using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    [CreateAssetMenu(fileName = "New Tile Setting", menuName = "MADE HIGHLOW/Tile Setting", order = 0)]
    public class TileSetting : ScriptableObject
    {
        public Sprite icon;
        public string displayName;

        [TextArea]
        public string description;
    }
}