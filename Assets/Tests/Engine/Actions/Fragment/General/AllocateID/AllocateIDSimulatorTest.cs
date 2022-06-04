using Moq;
using NUnit.Framework;

namespace RineaR.MadeHighlow.Actions.AllocateID
{
    public class AllocateIDSimulatorTest
    {
        [Test]
        public void Simulate_Always_ReturnsUpdated()
        {
            var context = new Mock<ISimulationContext>().Object;
            var world = WorldGenerator.Empty with { LatestAllocatedID = ID.From(1) };
            var result = new AllocateIDResult(ID.From(2));
            var simulator = new AllocateIDSimulator(context, world, result);

            var actual = simulator.Simulate();

            var expected = world with { LatestAllocatedID = ID.From(2) };
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
