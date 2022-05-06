namespace RineaR.MadeHighlow
{
    public record GenerateTileResult(in Tile GeneratedTile) : Result
    {
        public override World Simulate(in World world)
        {
            return GeneratedTile.CreateIn(world);
        }
    }
}