using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using RineaR.MadeHighlow.GameModel;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace RineaR.MadeHighlow.Clients.Local.Strategy
{
    public class FigureView : MonoBehaviour
    {
        [Header("States")]
        public Figure source;

        public Card selectedCard;

        [Header("Views")]
        public Image faceImage;

        public Image iconImage;
        public TMP_Text nameText;
        public Image healthGauge;
        public TMP_Text healthText;
        public CardView cardView;

        [Header("Interfaces")]
        public CardDropTarget cardDropArea;

        public CardArranger cardArranger;

        private CancellationTokenSource _cancellationTokenSource;
        private Subject<CardDragger> _onCardSet;
        public IObservable<CardDragger> OnCardSet => _onCardSet;

        private void Awake()
        {
            _onCardSet = new Subject<CardDragger>();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        private void Start()
        {
            cardView = GetComponentInChildren<CardView>();
            cardDropArea = GetComponentInChildren<CardDropTarget>();
            if (cardDropArea != null) cardDropArea.OnDropAsObservable().Subscribe(OnCardDropped).AddTo(this);
        }

        private void Update()
        {
            cardView.source = selectedCard;
            SetElements();
        }

        private void OnDestroy()
        {
            _onCardSet.Dispose();
        }

        private void OnCardDropped(CardDragger dragger)
        {
            if (source == null || dragger == null) return;
            _onCardSet.OnNext(dragger);
            cardDropArea.droppable = false;
            cardArranger.Arrange(dragger.view.source, source, _cancellationTokenSource.Token).Forget();
        }

        private void SetElements()
        {
            if (source == null)
            {
                ResetElements();
                return;
            }

            faceImage.sprite = source.setting.faceImage;
            iconImage.sprite = source.setting.iconImage;
            nameText.text = source.setting.displayName;
            healthGauge.fillAmount = (float)source.life.health / source.life.maxHealth;
            healthText.text = Mathf.Clamp(source.life.health, 0, 999).ToString();
        }

        private void ResetElements()
        {
            faceImage.sprite = null;
            iconImage.sprite = null;
            nameText.text = null;
            healthGauge.fillAmount = 0;
            healthText.text = null;
        }

        public void SetVisible(bool visible)
        {
            gameObject.SetActive(visible);
        }
    }
}