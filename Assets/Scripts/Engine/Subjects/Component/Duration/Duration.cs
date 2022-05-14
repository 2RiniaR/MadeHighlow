using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     期限
    /// </summary>
    public abstract record Duration
    {
        [CanBeNull]
        public abstract Duration Decrement();
    }
}
