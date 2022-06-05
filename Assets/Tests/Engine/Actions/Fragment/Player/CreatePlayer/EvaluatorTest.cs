using Moq;
using NUnit.Framework;

namespace RineaR.MadeHighlow.Actions.CreatePlayer
{
    public class EvaluatorTest
    {
        private static void SetupAllocateID(
            Mock<IEvaluationContext> contextMock,
            Mock<IHistory> historyMock,
            IHistory nextHistory
        )
        {
            contextMock.Setup(context => context.Actions.AllocateID(It.IsAny<IHistory>()))
                .Returns(Constants.AllocateIDEvent.Content);
            historyMock.SetupNextEvent(Constants.AllocateIDEvent, nextHistory);
        }

        private static void SetupRegisterPlayer(Mock<IHistory> historyMock, IHistory nextHistory)
        {
            historyMock.SetupNextEvent(Constants.RegisterPlayerEvent, nextHistory);
        }

        private static void SetupCreateComponentSucceed(
            Mock<IEvaluationContext> contextMock,
            Mock<IHistory> historyMock,
            IHistory nextHistory
        )
        {
            contextMock.SetupSequence(
                    context => context.Actions.CreateComponent(It.IsAny<IHistory>(), It.IsAny<CreateComponent.Action>())
                )
                .Returns(Constants.CreateComponentEvent1.Content)
                .Returns(Constants.CreateComponentEvent2.Content)
                .Returns(Constants.CreateComponentEvent3.Content);

            var historyMock3 = new Mock<IHistory>();
            historyMock3.SetupNextEvent(Constants.CreateComponentEvent3, nextHistory);
            var historyMock2 = new Mock<IHistory>();
            historyMock2.SetupNextEvent(Constants.CreateComponentEvent2, historyMock3.Object);
            var historyMock1 = historyMock;
            historyMock1.SetupNextEvent(Constants.CreateComponentEvent1, historyMock2.Object);
        }

        private static void SetupCreateComponentFailed(
            Mock<IEvaluationContext> contextMock,
            Mock<IHistory> historyMock,
            IHistory nextHistory
        )
        {
            contextMock.SetupSequence(
                    context => context.Actions.CreateComponent(It.IsAny<IHistory>(), It.IsAny<CreateComponent.Action>())
                )
                .Returns(Constants.CreateComponentEvent1.Content)
                .Returns(Constants.CreateComponentEvent2Failed.Content)
                .Returns(Constants.CreateComponentEvent3.Content);

            var historyMock3 = new Mock<IHistory>();
            historyMock3.SetupNextEvent(Constants.CreateComponentEvent3, nextHistory);
            var historyMock2 = new Mock<IHistory>();
            historyMock2.SetupNextEvent(Constants.CreateComponentEvent2Failed, historyMock3.Object);
            var historyMock1 = historyMock;
            historyMock1.SetupNextEvent(Constants.CreateComponentEvent1, historyMock2.Object);
        }

        [Test]
        public void Evaluate_Valid_ReturnsSucceed()
        {
            var contextMock = new Mock<IEvaluationContext>();
            var historyMock2 = new Mock<IHistory>();
            SetupCreateComponentSucceed(contextMock, historyMock2, Mock.Of<IHistory>());
            var historyMock1 = new Mock<IHistory>();
            SetupRegisterPlayer(historyMock1, historyMock2.Object);
            var historyMock0 = new Mock<IHistory>();
            SetupAllocateID(contextMock, historyMock0, historyMock1.Object);

            var action = new Action(Constants.InitialProps);
            var evaluator = new Evaluator(contextMock.Object, historyMock0.Object, action);

            var actual = evaluator.Evaluate();

            var expected = Constants.SucceedResult(action);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Evaluate_ComponentIncomplete_ReturnsFailed()
        {
            var contextMock = new Mock<IEvaluationContext>();
            var historyMock2 = new Mock<IHistory>();
            SetupCreateComponentFailed(contextMock, historyMock2, Mock.Of<IHistory>());
            var historyMock1 = new Mock<IHistory>();
            SetupRegisterPlayer(historyMock1, historyMock2.Object);
            var historyMock0 = new Mock<IHistory>();
            SetupAllocateID(contextMock, historyMock0, historyMock1.Object);

            var action = new Action(Constants.InitialProps);
            var evaluator = new Evaluator(contextMock.Object, historyMock0.Object, action);

            var actual = evaluator.Evaluate();

            var expected = Constants.FailedResult(action);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
