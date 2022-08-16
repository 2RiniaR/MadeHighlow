using System.Collections.Generic;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    public class League : MonoBehaviour
    {
        public List<Figure> figures;
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

        public void Join(Figure figure)
        {
            if (figure.transform.parent != transform)
            {
                Debug.LogWarning("The joining figure must be place in children of the league.");
                return;
            }

            figures.Add(figure);
        }

        public void Leave(Figure figure)
        {
            figures.Remove(figure);
        }

        public static League ContainerOf(Figure figure)
        {
            return figure.GetComponentInParent<League>();
        }
    }
}