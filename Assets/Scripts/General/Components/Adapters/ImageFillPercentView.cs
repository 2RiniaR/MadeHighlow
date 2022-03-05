using System;
using UnityEngine.UI;

namespace General.Components.Adapters
{
    [Serializable]
    public class ImageFillPercentView : IPercentView
    {
        public Image image;

        public float Percent
        {
            get => image.fillAmount;
            set => image.fillAmount = value;
        }
    }
}