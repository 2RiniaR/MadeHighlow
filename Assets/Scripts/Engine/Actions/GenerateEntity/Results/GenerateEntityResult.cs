namespace RineaR.MadeHighlow
{
    public record GenerateEntityResult(in Entity GeneratedEntity) : Result
    {
        public override World Simulate(in World world)
        {
            return GeneratedEntity.CreateIn(world);
        }
    }
}