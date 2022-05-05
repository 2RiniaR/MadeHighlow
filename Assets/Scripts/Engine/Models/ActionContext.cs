using System;

namespace RineaR.MadeHighlow
{
    public class ActionContext : IActionContext
    {
        public ActionContext(Session session)
        {
            Session = session;
        }

        public Session Session { get; }

        public World World => throw new NotImplementedException();

        public World WorldAt(ID id)
        {
            throw new NotImplementedException();
        }

        public IActionContext Appended(Result result)
        {
            throw new NotImplementedException();
        }

        public IActionContext AppendedRange(params Result[] results)
        {
            throw new NotImplementedException();
        }

        public ID NewID()
        {
            throw new NotImplementedException();
        }

        public float GetRandom()
        {
            throw new NotImplementedException();
        }
    }
}