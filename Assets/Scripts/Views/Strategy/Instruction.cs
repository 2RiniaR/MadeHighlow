using General.Adapters;
using UnityEngine;

namespace Views.Strategy
{
    public class Instruction : MonoBehaviour
    {
        [SerializeField, Header("Components")]
        private TextViewer contentText;

        [Header("States"), Space] [SerializeField]
        private string content;

        public string Content
        {
            get => content;
            set => content = value;
        }

        private void Update()
        {
            UpdateContentView();
        }

        private void UpdateContentView()
        {
            contentText.Content = content;
        }
    }
}