using System;
using System.Collections.Generic;
using RineaR.MadeHighlow.GameData.Commands;
using RineaR.MadeHighlow.GameModel;
using UnityEngine;

namespace RineaR.MadeHighlow.GameData.CardEffects
{
    [RequireComponent(typeof(Card))]
    public class WalkActivator : MonoBehaviour
    {
        [Header("Requirements")]
        public Session session;

        public Card card;

        [Header("Settings")]
        public WalkRunner originalRunner;

        [Min(0)]
        public int costLimit;

        private void Reset()
        {
            RefreshReferences();
        }

        private void Start()
        {
            RefreshReferences();
            card.optionText = costLimit.ToString();
        }

        private void RefreshReferences()
        {
            card = GetComponent<Card>() ?? throw new NullReferenceException();
            session ??= GetComponentInParent<Session>();
        }

        public WalkRunner Activate(Figure walker, WalkRoute route)
        {
            var walkRunner = Instantiate(originalRunner);
            walkRunner.name = originalRunner.name;
            walkRunner.Route = route;
            walkRunner.costLimit = costLimit;
            walkRunner.command.payCards = new List<Card> { card };
            walkRunner.command.quickness = card.quickness;
            walkRunner.command.figure = walker;
            walkRunner.command.session = session;
            return walkRunner;
        }
    }
}