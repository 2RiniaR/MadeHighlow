using Moq;
using NUnit.Framework;

namespace RineaR.MadeHighlow.Actions.RegisterComponent
{
    public class RegisterComponentSimulatorTest
    {
        [Test]
        public void Simulate_Always_ReturnsUpdated()
        {
            var context = new Mock<ISimulationContext>().Object;
            var player = PlayerGenerator.Empty with { ID = ID.From(1) };
            var registered = ComponentGenerator.Empty with { ID = ID.From(2), AttachedID = player.PlayerID };
            var world = WorldGenerator.Empty with { Players = new ValueList<Player>(player) };
            var result = new SucceedResult(RegisterComponentActionGenerator.Empty, registered);
            var simulator = new Simulator(context, world, result);

            var actual = simulator.Simulate();

            var afterPlayer = player with { Components = new ValueList<Component>(registered) };
            var expected = world with { Players = new ValueList<Player>(afterPlayer) };
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
