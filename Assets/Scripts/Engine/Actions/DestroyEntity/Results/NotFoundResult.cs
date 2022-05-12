namespace RineaR.MadeHighlow.Actions.DestroyEntity
{
    public record NotFoundResult(EntityID TargetID) : DestroyEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
