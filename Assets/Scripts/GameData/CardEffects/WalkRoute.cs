using System.Collections.Generic;
using RineaR.MadeHighlow.GameModel.Geometry;

namespace RineaR.MadeHighlow.GameData.CardEffects
{
    public class WalkRoute
    {
        private readonly List<FieldDirection2> _directions;

        public WalkRoute(IEnumerable<FieldDirection2> directions)
        {
            _directions = new List<FieldDirection2>(directions);
            Length = _directions.Count;
            Valid = Validation();
        }

        public IReadOnlyList<FieldDirection2> Directions => _directions;
        public int Length { get; }
        public bool Valid { get; }

        private bool Validation()
        {
            // 直前の移動方向と逆方向には、移動することができない。
            // 
            return true;
        }

        public override string ToString()
        {
            var result = "WalkRoute:";
            for (var i = 0; i < _directions.Count; i++)
            {
                result += $"\n[{i}] {_directions[i].ToString()}";
            }

            return result;
        }
    }
}