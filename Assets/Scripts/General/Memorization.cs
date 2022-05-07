using System;

namespace General
{
    public class Memorization<T>
    {
        private readonly Func<T> _getter;
        public bool ThrowOnNull { get; set; } = false;

        public Memorization(Func<T> getter)
        {
            _getter = getter;
        }

        private T _memorized;

        public T Value
        {
            get
            {
                if (_memorized != null)
                {
                    return _memorized;
                }

                _memorized = _getter();
                if (_memorized == null && ThrowOnNull)
                {
                    throw new NullReferenceException("`getter` returned value of null.");
                }

                return _memorized;
            }
        }
    }
}
