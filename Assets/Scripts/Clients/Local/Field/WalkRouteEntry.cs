using System;
using System.Collections.Generic;
using System.Linq;
using RineaR.MadeHighlow.GameData.CardEffects;
using RineaR.MadeHighlow.GameModel;
using RineaR.MadeHighlow.GameModel.Geometry;
using UniRx;

namespace RineaR.MadeHighlow.Clients.Local.Field
{
    public class WalkRouteEntry : IDisposable
    {
        private readonly List<WalkRouteCheckpoint> _checkpoints = new();
        private readonly Subject<WalkRoute> _onUpdated = new();

        public WalkRouteEntry(Figure walker)
        {
            Walker = walker;
            ResetCheckpoints();
        }

        public Figure Walker { get; }
        public WalkRoute Current { get; private set; }
        public IObservable<WalkRoute> OnUpdated => _onUpdated;
        public IReadOnlyList<WalkRouteCheckpoint> Checkpoints => _checkpoints;
        public FieldVector2 LatestCheckpoint => _checkpoints[^1].Destination;

        public void Dispose()
        {
            _onUpdated.Dispose();
        }

        public void AddCheckpoint(FieldVector2 position)
        {
            var previous = _checkpoints.Count > 0 ? _checkpoints[^1] : null;
            var checkpoint = new WalkRouteCheckpoint(Walker, position, previous);
            _checkpoints.Add(checkpoint);
            UpdateRoute();
        }

        public void UndoCheckpoint()
        {
            _checkpoints.RemoveAt(_checkpoints.Count - 1);
            UpdateRoute();
        }

        public void ResetCheckpoints()
        {
            _checkpoints.Clear();
            UpdateRoute();
        }

        private void UpdateRoute()
        {
            var directions = Checkpoints.SelectMany(checkpoint => checkpoint.Path);
            var route = new WalkRoute(Walker, directions);
            Current = route;
            _onUpdated.OnNext(route);
        }
    }
}