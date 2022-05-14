namespace RineaR.MadeHighlow.Actions.Valid.DestroyTile
{
    public record NotFoundResult(TileID TargetID) : DestroyTileResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
