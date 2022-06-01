using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public interface IPredictor
    {
        [ItemNotNull]
        [NotNull]
        public ValueList<IValidAction> PredictionsOn([NotNull] IValidAction action);
    }
}
