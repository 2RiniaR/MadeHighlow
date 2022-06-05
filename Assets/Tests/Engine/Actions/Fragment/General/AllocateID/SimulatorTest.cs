using Moq;
using NUnit.Framework;

namespace RineaR.MadeHighlow.Actions.AllocateID
{
    public class SimulatorTest
    {
        [Test]
        public void Simulate_Always_ReturnsUpdated()
        {
            var context = new Mock<ISimulationContext>().Object;
            var world = WorldGenerator.Empty with { NextID = ID.From(1) };
            var result = new Result(ID.From(1));
            var simulator = new Simulator(context, world, result);

            var actual = simulator.Simulate();

            var expected = world with { NextID = ID.From(2) };
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
