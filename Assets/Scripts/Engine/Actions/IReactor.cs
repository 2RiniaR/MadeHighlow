using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public interface IReactor
    {
        [ItemNotNull]
        [NotNull]
        public ValueList<ValidAction> ReactionsOn([NotNull] ValidResult result);
    }
}
