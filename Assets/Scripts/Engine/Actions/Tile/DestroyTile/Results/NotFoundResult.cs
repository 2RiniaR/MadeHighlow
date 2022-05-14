namespace RineaR.MadeHighlow.Actions.DestroyTile
{
    public record NotFoundResult(TileID TargetID) : DestroyTileResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
