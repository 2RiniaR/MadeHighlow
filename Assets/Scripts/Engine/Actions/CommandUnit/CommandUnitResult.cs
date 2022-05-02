namespace RineaR.MadeHighlow.Actions
{
    public abstract record CommandUnitResult(CommandUnitResultCode Code) : Result(ActionType.CommandUnit);
}