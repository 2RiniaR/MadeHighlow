namespace Game.Primitives
{
    public readonly struct FieldPosition
    {
        private readonly int _x;
        private readonly int _y;

        public FieldPosition(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public bool Equals(FieldPosition other)
        {
            return _x == other._x && _y == other._y;
        }

        public override int GetHashCode()
        {
            return _x.GetHashCode() ^ _y.GetHashCode();
        }

        public FieldPosition Add(FieldPosition other) => new FieldPosition(_x + other._x, _y + other._y);
        public FieldPosition Multiple(int value) => new FieldPosition(_x * value, _y * value);
        public FieldPosition Inverse() => Multiple(-1);
        public static FieldPosition Zero => new FieldPosition(0, 0);
    }
}
