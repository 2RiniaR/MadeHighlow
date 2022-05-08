using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface IReactor
    {
        [ItemNotNull]
        [NotNull]
        public ValueList<Action> ReactionsOn([NotNull] Result result);
    }
}
