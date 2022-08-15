using RineaR.MadeHighlow.GameModel;
using UnityEngine;

namespace RineaR.MadeHighlow.Clients.Local.Field
{
    public class Focus
    {
        private Collider _previousHit;
        private Tile _previousTarget;

        public Tile Current { get; set; }
        public float MaxDistance { get; set; } = Mathf.Infinity;
        public LayerMask TargetLayerMask { get; set; } = 0;

        public void SetTileAt(Vector2 screenPosition)
        {
            Current = GetFocusTile(screenPosition);
        }

        public Tile GetFocusTile(Vector2 screenPosition)
        {
            if (Camera.main == null) return null;
            var ray = Camera.main.ScreenPointToRay(screenPosition);
            if (!Physics.Raycast(ray, out var hit, MaxDistance, TargetLayerMask)) return null;

            if (hit.collider == _previousHit) return _previousTarget;
            _previousHit = hit.collider;

            var target = hit.collider.GetComponentInParent<Tile>();
            _previousTarget = target;
            return target;
        }
    }
}