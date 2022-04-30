using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions;

namespace RineaR.MadeHighlow
{
    public interface ISessionHolder
    {
        [NotNull]
        public Session Current();

        public void Append([NotNull] in Event timeline);
    }
}