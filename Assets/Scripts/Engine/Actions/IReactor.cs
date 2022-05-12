using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public interface IReactor
    {
        [ItemNotNull]
        [NotNull]
        public ValueList<Action> ReactionsOn([NotNull] Result result);
    }
}
