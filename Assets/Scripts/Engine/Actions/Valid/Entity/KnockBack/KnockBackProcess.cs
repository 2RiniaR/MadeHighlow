namespace RineaR.MadeHighlow.Actions.KnockBack
{
    public record KnockBackProcess(Event<ReactedResult<EntityFly.SucceedResult>> EntityFlyEvent)
    {
        public Timeline Timeline { get; } = new Timeline().Then(EntityFlyEvent);
    }
}
