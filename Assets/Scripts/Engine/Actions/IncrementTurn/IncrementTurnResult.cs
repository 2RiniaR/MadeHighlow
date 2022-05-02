namespace RineaR.MadeHighlow.Actions
{
    public record IncrementTurnResult() : Result(ActionType.IncrementTurn)
    {
        public override World Simulate(in World world)
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
                                .ToValueObjectList(),
                        }
                    )
                    .ToValueObjectList(),
                CurrentTurn = world.CurrentTurn.Increment(),
            };
        }
    }
}