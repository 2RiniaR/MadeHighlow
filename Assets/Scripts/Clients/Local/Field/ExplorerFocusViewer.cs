using RineaR.MadeHighlow.GameModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RineaR.MadeHighlow.Clients.Local.Field
{
    public class ExplorerFocusViewer : MonoBehaviour
    {
        public Tile source;
        public Image iconImage;
        public TMP_Text nameText;
        public TMP_Text descriptionText;

        private void Update()
        {
            if (source == null)
            {
                iconImage.sprite = null;
                nameText.text = null;
                descriptionText.text = null;
                return;
            }

            iconImage.sprite = source.setting.icon;
            nameText.text = source.setting.displayName;
            descriptionText.text = source.setting.description;
        }
    }
}