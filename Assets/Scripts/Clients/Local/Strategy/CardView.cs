using RineaR.MadeHighlow.GameModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RineaR.MadeHighlow.Clients.Local.Strategy
{
    public class CardView : MonoBehaviour
    {
        [Header("States")] public Card source;

        [Header("Views")] public Image baseImage;
        public Image frameImage;
        public Image commandImage;
        public Image quicknessImage;
        public Image shadowImage;
        public TextMeshProUGUI optionText;

        private void Update()
        {
            SetElements();
        }

        private void SetElements()
        {
            if (source == null)
            {
                ResetElements();
                return;
            }

            baseImage.sprite = source.genre.baseImage;
            shadowImage.sprite = source.genre.shadowImage;
            commandImage.sprite = source.setting.iconImage;
            frameImage.sprite = source.setting.frameImage;
            quicknessImage.sprite = source.quickness != null ? source.quickness.indicatorImage : null;
            optionText.text = source.optionText;
        }

        private void ResetElements()
        {
            baseImage.sprite = null;
            shadowImage.sprite = null;
            commandImage.sprite = null;
            frameImage.sprite = null;
            quicknessImage.sprite = null;
            optionText.text = null;
        }

        public void SetVisible(bool visible)
        {
            gameObject.SetActive(visible);
        }
    }
}