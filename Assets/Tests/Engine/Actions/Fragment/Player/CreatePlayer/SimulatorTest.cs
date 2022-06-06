using Moq;
using NUnit.Framework;

namespace RineaR.MadeHighlow.Actions.CreatePlayer
{
    public class SimulatorTest
    {
        private static void SetupCreate(Mock<ISimulationContext> contextMock)
        {
            contextMock.Setup(context => context.Modifier.CreatePlayer(Constants.BeforeWorld, Constants.CreatedPlayer))
                .Returns(Constants.AfterWorld);
        }

        [Test]
        public void Simulate_Valid_ReturnsChanged()
        {
            var contextMock = new Mock<ISimulationContext>();
            SetupCreate(contextMock);
            var simulator = new Simulator(
                contextMock.Object,
                Constants.BeforeWorld,
                Constants.SucceedResult(Mock.Of<IAction>())
            );

            var actual = simulator.Simulate();

            Assert.That(actual, Is.EqualTo(Constants.AfterWorld));
        }

        [Test]
        public void Simulate_Failed_ReturnsNotChanged()
        {
            var contextMock = new Mock<ISimulationContext>();
            SetupCreate(contextMock);
            var simulator = new Simulator(
                contextMock.Object,
                Constants.BeforeWorld,
                Constants.FailedResult(Mock.Of<IAction>())
            );

            var actual = simulator.Simulate();

            Assert.That(actual, Is.EqualTo(Constants.BeforeWorld));
        }
    }
}
