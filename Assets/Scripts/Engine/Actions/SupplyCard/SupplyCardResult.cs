namespace RineaR.MadeHighlow.Actions
{
    public abstract record SupplyCardResult(in SupplyCardResultCode Code) : Result(ActionType.SupplyCard);
}