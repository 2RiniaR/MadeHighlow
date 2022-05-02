using System;
using RineaR.MadeHighlow.Actions;

namespace RineaR.MadeHighlow
{
    public class SessionModel : ISessionModel
    {
        public SessionModel(Session session)
        {
            Session = session;
        }

        public Session Session { get; }

        public World Current()
        {
            throw new NotImplementedException();
        }

        public World SnapshotAt(ID<SessionEvent> id)
        {
            throw new NotImplementedException();
        }

        public void Advance(Result result)
        {
            throw new NotImplementedException();
        }

        public void AdvanceRange(params Result[] results)
        {
            throw new NotImplementedException();
        }

        public ID<T> NewID<T>()
        {
            throw new NotImplementedException();
        }
    }
}