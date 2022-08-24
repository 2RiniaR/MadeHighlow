using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    /// <summary>
    ///     選択可能な開始地点
    /// </summary>
    [RequireComponent(typeof(Tile))]
    public class LandingPoint : MonoBehaviour
    {
        [Header("Requirements")]
        public Tile tile;

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
            tile = this.GetRequireComponent<Tile>();
        }
    }
}