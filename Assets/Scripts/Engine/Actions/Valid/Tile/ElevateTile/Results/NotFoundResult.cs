namespace RineaR.MadeHighlow.Actions.Valid.ElevateTile
{
    public record NotFoundResult(TileID TargetID) : ElevateTileResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
