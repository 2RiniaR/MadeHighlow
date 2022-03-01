using GameView.Structures;
using General.Components;
using General.Components.Adapters;
using UnityEngine;

namespace GameView.Strategy.Units
{
    public class UnitEffectIcon : MonoBehaviour
    {
        [Header("Components")]
        [SerializeReference] public ISpriteView IconImage = new UIImageSpriteView();

        [Header("States"), Space]
        public UnitEffectData effect;
        public int duration;

        private void Update()
        {
            UpdateObjectName();
            UpdateIconImage();
        }

        private void UpdateObjectName()
        {
            gameObject.name = $"{effect.displayName}";
        }

        private void UpdateIconImage()
        {
            IconImage.Image = effect.icon;
        }
    }
}