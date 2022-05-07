using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface ITurnUpdateReactor
    {
        public TurnUpdateReaction OnTurnUpdate([NotNull] IActionContext session);
    }
}
