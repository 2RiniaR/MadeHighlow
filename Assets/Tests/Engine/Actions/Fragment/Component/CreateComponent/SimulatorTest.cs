using Moq;
using NUnit.Framework;

namespace RineaR.MadeHighlow.Actions.CreateComponent
{
    public class SimulatorTest
    {
        private static void SetupCreate(Mock<ISimulationContext> contextMock)
        {
            contextMock.Setup(
                    context => context.Modifier.CreateComponent(Constants.BeforeWorld, Constants.CreatedComponent)
                )
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
        public void Simulate_Rejected_ReturnsNotChanged()
        {
            var contextMock = new Mock<ISimulationContext>();
            SetupCreate(contextMock);
            var context = contextMock.Object;
            var simulator = new Simulator(context, Constants.BeforeWorld, Constants.RejectedResult(Mock.Of<IAction>()));

            var actual = simulator.Simulate();

            var expected = Constants.BeforeWorld;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Simulate_RegisterFailed_ReturnsNotChanged()
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
