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
        [Header("Requirements")] public Card card;

        [Header("Settings")] public WalkRunner originalRunner;
        [Min(0)] public int availableCost;

        public ISession Session { get; private set; }

        private void Reset()
        {
            RefreshReferences();
        }

        private void Start()
        {
            RefreshReferences();
            card.optionText = availableCost.ToString();
        }

        private void RefreshReferences()
        {
            card = GetComponent<Card>() ?? throw new NullReferenceException();
            Session = GameModel.Session.ContextOf(this);
        }

        public WalkRunner Activate(WalkRoute route)
        {
            var walkRunner = Instantiate(originalRunner);
            walkRunner.name = originalRunner.name;
            walkRunner.Route = route;
            walkRunner.command.payCards = new List<Card> { card };
            walkRunner.command.quickness = card.quickness;
            walkRunner.command.figure = route.Walker;
            return walkRunner;
        }
    }
}