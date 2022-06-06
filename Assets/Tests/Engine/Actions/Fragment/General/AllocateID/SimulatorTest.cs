using Moq;
using NUnit.Framework;

namespace RineaR.MadeHighlow.Actions.AllocateID
{
    public class SimulatorTest
    {
        [Test]
        public void Simulate_Always_ReturnsUpdated()
        {
            var simulator = new Simulator(
                Mock.Of<ISimulationContext>(),
                Constants.BeforeWorld,
                Constants.SucceedResult
            );

            var actual = simulator.Simulate();

            Assert.That(actual, Is.EqualTo(Constants.AfterWorld));
        }
    }
}
