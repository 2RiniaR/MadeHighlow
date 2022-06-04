using Moq;
using NUnit.Framework;

namespace RineaR.MadeHighlow.Actions.RegisterPlayer
{
    public class RegisterPlayerSimulatorTest
    {
        [Test]
        public void Simulate_Always_ReturnsUpdated()
        {
            var context = new Mock<ISimulationContext>().Object;
            var world = WorldGenerator.Empty with { Players = ValueList<Player>.Empty };
            var registered = PlayerGenerator.Empty with { ID = ID.From(1) };
            var result = new Result(RegisterPlayerActionGenerator.Empty, registered);
            var simulator = new Simulator(context, world, result);

            var actual = simulator.Simulate();

            var expected = world with { Players = new ValueList<Player>(registered) };
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
