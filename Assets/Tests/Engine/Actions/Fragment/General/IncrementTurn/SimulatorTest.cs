using Moq;
using NUnit.Framework;

namespace RineaR.MadeHighlow.Actions.IncrementTurn
{
    public class SimulatorTest
    {
        [Test]
        public void Simulate_Always_ReturnsUpdated()
        {
            var context = new Mock<ISimulationContext>().Object;
            var world = WorldGenerator.Empty with { CurrentTurn = new Turn(0) };
            var result = new Result(new Turn(1));
            var simulator = new Simulator(context, world, result);

            var actual = simulator.Simulate();

            var expected = world with { CurrentTurn = new Turn(1) };
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
