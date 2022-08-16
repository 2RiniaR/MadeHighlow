using System.Collections.Generic;
using JetBrains.Annotations;
using RineaR.MadeHighlow.GameModel;
using RineaR.MadeHighlow.GameModel.Geometry;
using UnityEngine;

namespace RineaR.MadeHighlow.Clients.Local.Field
{
    public class WalkRouteCheckpoint
    {
        public WalkRouteCheckpoint(Figure walker, FieldVector2 destination,
            [CanBeNull] WalkRouteCheckpoint previousCheckpoint)
        {
            Walker = walker;
            Destination = destination;
            PreviousCheckpoint = previousCheckpoint;
            StartPosition = PreviousCheckpoint?.Destination ?? Walker.fieldTransform.position.To2D();
            Path = ResolvePath();
        }

        public Figure Walker { get; }
        public FieldVector2 Destination { get; }
        public FieldVector2 StartPosition { get; }
        public WalkRouteCheckpoint PreviousCheckpoint { get; }
        public IReadOnlyList<FieldDirection2> Path { get; }

        private IReadOnlyList<FieldDirection2> ResolvePath()
        {
            var difference = Destination - StartPosition;
            var path = new List<FieldDirection2>();
            var xSign = (int)Mathf.Sign(difference.horizontal);
            for (var i = 0; i < Mathf.Abs(difference.horizontal); i++)
                path.Add(FieldDirection2.FromVector(new FieldVector2 { horizontal = xSign, vertical = 0 }));
            var ySign = (int)Mathf.Sign(difference.vertical);
            for (var i = 0; i < Mathf.Abs(difference.vertical); i++)
                path.Add(FieldDirection2.FromVector(new FieldVector2 { horizontal = 0, vertical = ySign }));
            return path;
        }
    }
}