using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.SupplyCard
{
    public record PlaceCardFailedResult([NotNull] Action Action, [NotNull] PlaceCard.Result Failed) : Result;
}
