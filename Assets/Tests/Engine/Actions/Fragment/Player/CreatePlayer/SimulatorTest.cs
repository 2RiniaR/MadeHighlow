using Moq;
using NUnit.Framework;

namespace RineaR.MadeHighlow.Actions.CreatePlayer
{
    public class SimulatorTest
    {
        private static void SetupCreate(Mock<ISimulationContext> contextMock)
        {
            contextMock.Setup(context => context.Modifier.CreatePlayer(Constants.BeforeWorld, Constants.Created))
                .Returns(Constants.AfterWorld);
        }

        [Test]
        public void Simulate_Valid_ReturnsChanged()
        {
            var contextMock = new Mock<ISimulationContext>();
            SetupCreate(contextMock);
            var context = contextMock.Object;
            var simulator = new Simulator(context, Constants.BeforeWorld, Constants.SucceedResult(Mock.Of<IAction>()));

            var actual = simulator.Simulate();

            var expected = Constants.AfterWorld;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Simulate_Failed_ReturnsNotChanged()
        {
            var contextMock = new Mock<ISimulationContext>();
            SetupCreate(contextMock);
            var context = contextMock.Object;
            var simulator = new Simulator(context, Constants.BeforeWorld, Constants.FailedResult(Mock.Of<IAction>()));

            var actual = simulator.Simulate();

            var expected = Constants.BeforeWorld;
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
