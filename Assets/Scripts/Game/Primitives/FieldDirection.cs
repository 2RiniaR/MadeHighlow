namespace Game.Primitives
{
    public readonly struct FieldDirection
    {
        private enum Type
        {
            Up,
            Down,
            Right,
            Left
        }

        private readonly Type _type;

        private FieldDirection(Type type)
        {
            _type = type;
        }

        public bool Equals(FieldDirection other)
        {
            return _type == other._type;
        }

        public override int GetHashCode()
        {
            return _type.GetHashCode();
        }

        public static FieldDirection Up => new FieldDirection(Type.Up);
        public static FieldDirection Down => new FieldDirection(Type.Down);
        public static FieldDirection Right => new FieldDirection(Type.Right);
        public static FieldDirection Left => new FieldDirection(Type.Left);
    }
}
