namespace RineaR.MadeHighlow
{
    public record IncrementTurnResult : Result
    {
        public override World Simulate(World world)
        {
            return world with
            {
                Entities = world.Entities.Select(
                        @object => @object with
                        {
                            Components = @object.Components.Select(
                                    component => component with
                                    {
                                        Duration = component.Duration.Decrement(),
                                    }
                                )
                                .ToValueList(),
                        }
                    )
                    .ToValueList(),
                CurrentTurn = world.CurrentTurn.Increment(),
            };
        }
    }
}
