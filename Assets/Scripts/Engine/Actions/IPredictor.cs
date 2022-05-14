using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public interface IPredictor
    {
        [ItemNotNull]
        [NotNull]
        public ValueList<ValidAction> PredictionsOn([NotNull] ValidAction action);
    }
}
