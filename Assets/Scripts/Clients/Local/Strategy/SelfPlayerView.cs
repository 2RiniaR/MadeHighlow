using RineaR.MadeHighlow.GameModel;
using UniRx;
using UnityEngine;

namespace RineaR.MadeHighlow.Clients.Local.Strategy
{
    public class SelfPlayerView : MonoBehaviour
    {
        [Header("Views")] public LeagueView leagueView;
        public DeckView deckView;

        private void Start()
        {
            leagueView.OnCardSetAsObservable().Subscribe(x => OnCardSet(x.figureView, x.cardDragger)).AddTo(this);
        }

        private void OnCardSet(FigureView figureView, CardDragger cardDragger)
        {
            figureView.selectedCard = cardDragger.view.source;
            cardDragger.view.SetVisible(false);
        }

        public void SetSource(Player source)
        {
            deckView.SetSource(source.deck);
            leagueView.SetSource(source.league);
        }

        private void ResetElements()
        {
            deckView.ResetElements();
            leagueView.ResetElements();
        }
    }
}