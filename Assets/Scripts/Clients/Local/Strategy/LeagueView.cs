using System;
using System.Collections.Generic;
using System.Linq;
using RineaR.MadeHighlow.GameModel;
using UniRx;
using UnityEngine;

namespace RineaR.MadeHighlow.Clients.Local.Strategy
{
    public class LeagueView : MonoBehaviour
    {
        [Header("Views")] public List<FigureView> figureViews;

        public void SetSource(League source)
        {
            if (source == null)
            {
                ResetElements();
                return;
            }

            for (var i = 0; i < figureViews.Count; i++)
            {
                var figureView = figureViews[i];
                if (source.figures.Count <= i)
                {
                    figureView.source = null;
                    figureView.SetVisible(false);
                }
                else
                {
                    figureView.SetVisible(true);
                    figureView.source = source.figures[i];
                }
            }
        }

        public void ResetElements()
        {
            foreach (var figureView in figureViews)
            {
                figureView.source = null;
                figureView.SetVisible(false);
            }
        }

        public IObservable<(FigureView figureView, CardDragger cardDragger)> OnCardSetAsObservable()
        {
            return figureViews.Select(figureView =>
                figureView.OnCardSet.Select(cardDragger => (figureView, cardDragger))).Merge();
        }
    }
}