namespace RineaR.MadeHighlow.Actions.KnockBack
{
    public record Process(Event<ReactedResult<EntityFly.SucceedResult>> EntityFlyEvent)
    {
        public Timeline Timeline { get; } = new Timeline().Then(EntityFlyEvent);
    }
}
