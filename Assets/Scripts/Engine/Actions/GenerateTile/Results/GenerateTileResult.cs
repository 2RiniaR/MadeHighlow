namespace RineaR.MadeHighlow
{
    public record GenerateTileResult(Tile GeneratedTile) : Result
    {
        public override World Simulate(World world)
        {
            return GeneratedTile.CreateIn(world);
        }
    }
}
