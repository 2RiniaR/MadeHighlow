using System.Collections.Generic;
using RineaR.MadeHighlow.GameModel;
using RineaR.MadeHighlow.GameModel.Geometry;

namespace RineaR.MadeHighlow.GameData.CardEffects
{
    public class WalkRoute
    {
        private readonly List<FieldDirection2> _directions;
        private readonly List<FieldVector2> _positions;

        public WalkRoute(Figure walker, IEnumerable<FieldDirection2> directions)
        {
            Walker = walker;

            _directions = new List<FieldDirection2>();
            _positions = new List<FieldVector2>();
            var currentPosition = walker.FieldTransform.position.To2D();
            foreach (var direction in directions)
            {
                _directions.Add(direction);
                currentPosition += direction.ToVector();
                _positions.Add(currentPosition);
            }

            Length = _directions.Count;
            Valid = Validation();
        }

        public IReadOnlyList<FieldDirection2> Directions => _directions;
        public IReadOnlyList<FieldVector2> Positions => _positions;
        public int Length { get; }
        public Figure Walker { get; }
        public bool Valid { get; }

        private bool Validation()
        {
            return true;
        }

        public override string ToString()
        {
            var result = "WalkRoute:";
            for (var i = 0; i < _directions.Count; i++) result += $"\n[{i}] {_directions[i].ToString()}";
            return result;
        }
    }
}