using System.Collections.Generic;
using RineaR.MadeHighlow.GameModel;
using UnityEngine;

namespace RineaR.MadeHighlow.Clients.Local.Strategy
{
    public class DeckView : MonoBehaviour
    {
        [Header("Views")]
        public List<CardView> cardViews;

        public void SetSource(Deck source)
        {
            if (source == null)
            {
                ResetElements();
                return;
            }

            for (var i = 0; i < cardViews.Count; i++)
            {
                var cardView = cardViews[i];
                if (source.cards.Count <= i)
                {
                    cardView.source = null;
                    cardView.SetVisible(false);
                }
                else
                {
                    cardView.SetVisible(true);
                    cardView.source = source.cards[i];
                }
            }
        }

        public void ResetElements()
        {
            foreach (var cardView in cardViews)
            {
                cardView.source = null;
                cardView.SetVisible(false);
            }
        }
    }
}