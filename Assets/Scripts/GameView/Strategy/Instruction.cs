using General.Components;
using General.Components.Adapters;
using UnityEngine;

namespace GameView.Strategy
{
    public class Instruction : MonoBehaviour
    {
        [Header("Components")] [SerializeReference]
        private ITextView _contentText = new TMPTextView();

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
            _contentText.Content = content;
        }
    }
}