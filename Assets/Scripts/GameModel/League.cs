using System.Collections.Generic;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    /// <summary>
    ///     複数のフィギュアで構成されるチーム
    /// </summary>
    public class League : MonoBehaviour
    {
        [Header("References on scene")]
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
            figures.Add(figure);
            this.LogInfo($"フィギュア（{figure.name}）がチームに参加しました。");
        }

        public void Leave(Figure figure)
        {
            figures.Remove(figure);
            this.LogInfo($"フィギュア（{figure.name}）がチームから脱退しました。");
        }
    }
}