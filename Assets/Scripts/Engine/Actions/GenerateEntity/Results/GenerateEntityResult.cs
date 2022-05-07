namespace RineaR.MadeHighlow
{
    public record GenerateEntityResult(Entity GeneratedEntity) : Result
    {
        public override World Simulate(World world)
        {
            return GeneratedEntity.CreateIn(world);
        }
    }
}