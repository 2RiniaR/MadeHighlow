using UnityEngine;

namespace GameView.Structures
{
    [CreateAssetMenu(fileName = "New Character", menuName = "MadeHighlow/Character")]
    public class CharacterData : ScriptableObject
    {
        public Sprite visualImage;
        public Sprite iconImage;
        public string displayName;
    }
}