namespace RineaR.MadeHighlow.Actions.IncrementTurn
{
    public record IncrementTurnEvent() : Event(ActionType.IncrementTurn)
    {
        public override World Simulate(in World world)
        {
            return world with
            {
                Objects = world.Objects.Items.ConvertAll(
                        @object => @object with
                        {
                            Components = @object.Components.Items.ConvertAll(
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