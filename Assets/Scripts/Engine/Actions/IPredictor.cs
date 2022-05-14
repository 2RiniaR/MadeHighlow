using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public interface IPredictor
    {
        [ItemNotNull]
        [NotNull]
        public ValueList<Action> PredictionsOn([NotNull] Action action);
    }
}
