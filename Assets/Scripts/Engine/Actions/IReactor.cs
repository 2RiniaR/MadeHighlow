using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public interface IReactor
    {
        [ItemNotNull]
        [NotNull]
        public ValueList<IValidAction> ReactionsOn([NotNull] IValidResult result);
    }
}
