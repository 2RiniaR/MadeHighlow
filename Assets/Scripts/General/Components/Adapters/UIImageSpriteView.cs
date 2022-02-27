using System;
using UnityEngine;
using UnityEngine.UI;

namespace General.Components.Adapters
{
    [Serializable]
    public class UIImageSpriteView : ISpriteView
    {
        public Image image;

        public Sprite Image
        {
            get => image.sprite;
            set => image.sprite = value;
        }
    }
}