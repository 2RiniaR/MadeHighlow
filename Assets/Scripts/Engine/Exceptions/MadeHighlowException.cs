using System;

namespace RineaR.MadeHighlow.Engine.Exceptions
{
    public class MadeHighlowException : Exception
    {
        public MadeHighlowException()
        {
        }

        public MadeHighlowException(string message) : base(message)
        {
        }
    }
}