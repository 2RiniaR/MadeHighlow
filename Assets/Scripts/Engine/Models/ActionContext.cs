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

        public World CurrentWorld()
        {
            throw new NotImplementedException();
        }

        public World WorldAt(ID id)
        {
            throw new NotImplementedException();
        }

        public void Append(ISimulatable result)
        {
            throw new NotImplementedException();
        }

        public void AppendRange(params ISimulatable[] results)
        {
            throw new NotImplementedException();
        }

        public ID NewID()
        {
            throw new NotImplementedException();
        }
    }
}