using UnityEngine;

namespace GameView.Structures
{
    [CreateAssetMenu(fileName = "New Unit effect", menuName = "MadeHighlow/Unit effect", order = 0)]
    public class UnitEffectData : ScriptableObject
    {
        public Sprite icon;
        public string displayName;
    }
}