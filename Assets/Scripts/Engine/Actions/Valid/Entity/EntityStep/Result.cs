using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.EvaluationFlows.CheckRejection;

namespace RineaR.MadeHighlow.Actions.EntityStep
{
    public record Result([NotNull] Action Action) : IValidResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new Simulator(context, world, this).Simulate();
        }

        public ValueList<Interrupt<CostEffect>> CostEffects { get; init; }
        public Cost Expended { get; init; }
        public ValueList<Event<MoveEntity.Result>> ClimbMoveEvents { get; init; }
        public Event<MoveEntity.Result> ShiftMoveEvent { get; init; }
        public ValueList<Event<MoveEntity.Result>> FallMoveEvents { get; init; }
        public Rejection Rejection { get; init; }
    }
}
