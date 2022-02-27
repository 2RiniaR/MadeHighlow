using System;
using TMPro;

namespace General.Components.Adapters
{
    [Serializable]
    public class TMPTextView : ITextView
    {
        public TMP_Text tmpText;

        public string Content
        {
            get => tmpText.text;
            set => tmpText.text = value;
        }
    }
}