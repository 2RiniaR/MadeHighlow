using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface IReactor
    {
        [ItemNotNull]
        [NotNull]
        public ValueObjectList<Action> ReactionsOn([NotNull] Result result);
    }
}
