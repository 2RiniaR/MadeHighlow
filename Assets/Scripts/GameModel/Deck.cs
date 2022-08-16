using System.Collections.Generic;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    public class Deck : MonoBehaviour
    {
        public List<Card> cards;
        public Player owner;

        private void Reset()
        {
            RefreshReferences();
        }

        private void Start()
        {
            RefreshReferences();
        }

        private void RefreshReferences()
        {
            owner ??= GetComponentInParent<Player>();
        }

        public void AddCard(Card card)
        {
            cards.Add(card);
        }

        public void PayCard(Card card)
        {
            cards.Remove(card);
        }

        public static Deck ContainerOf(Card card)
        {
            return card.GetComponentInParent<Deck>();
        }
    }
}