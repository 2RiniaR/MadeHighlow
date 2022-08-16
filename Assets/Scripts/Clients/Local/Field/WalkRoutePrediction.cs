using System.Collections.Generic;
using RineaR.MadeHighlow.GameData.CardEffects;
using RineaR.MadeHighlow.GameModel;
using RineaR.MadeHighlow.GameModel.Geometry;

namespace RineaR.MadeHighlow.Clients.Local.Field
{
    public class WalkRoutePrediction
    {
        private readonly List<FieldVector2> _positions;

        public WalkRoutePrediction(Figure walker, WalkRoute route)
        {
            Walker = walker;
            Route = route;

            _positions = new List<FieldVector2>();
            var currentPosition = walker.fieldTransform.position.To2D();
            foreach (var direction in Route.Directions)
            {
                currentPosition += direction.ToVector();
                _positions.Add(currentPosition);
            }

            Valid = Validation();
        }

        public WalkRoute Route { get; }
        public IReadOnlyList<FieldVector2> Positions => _positions;
        public Figure Walker { get; }
        public bool Valid { get; }

        private bool Validation()
        {
            return true;
        }
    }
}