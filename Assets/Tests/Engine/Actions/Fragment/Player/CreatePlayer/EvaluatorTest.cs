using Moq;
using NUnit.Framework;

namespace RineaR.MadeHighlow.Actions.CreatePlayer
{
    public class EvaluatorTest
    {
        private static void SetupAllocateID(Mock<IEvaluationContext> contextMock)
        {
            contextMock.Setup(context => context.Actions.AllocateID(It.IsAny<IHistory>()))
                .Returns(Constants.AllocateIDEvent.Content);
        }

        private static void SetupCreateComponentSucceed(Mock<IEvaluationContext> contextMock)
        {
            contextMock.SetupSequence(
                    context => context.Actions.CreateComponent(It.IsAny<IHistory>(), It.IsAny<CreateComponent.Action>())
                )
                .Returns(Constants.CreateComponentEvent1.Content)
                .Returns(Constants.CreateComponentEvent2.Content)
                .Returns(Constants.CreateComponentEvent3.Content);
        }

        private static void SetupCreateComponentFailed(Mock<IEvaluationContext> contextMock)
        {
            contextMock.SetupSequence(
                    context => context.Actions.CreateComponent(It.IsAny<IHistory>(), It.IsAny<CreateComponent.Action>())
                )
                .Returns(Constants.CreateComponentEvent1.Content)
                .Returns(Constants.CreateComponentEvent2Failed.Content)
                .Returns(Constants.CreateComponentEvent3.Content);
        }

        [Test]
        public void Evaluate_Valid_ReturnsSucceed()
        {
            var contextMock = new Mock<IEvaluationContext>();
            SetupAllocateID(contextMock);
            SetupCreateComponentSucceed(contextMock);

            var historyMock = new Mock<IHistory>();
            HistoryBuilder.New.UseMock(historyMock)
                .Then(Constants.AllocateIDEvent)
                .Then(Constants.RegisterPlayerEvent)
                .Then(Constants.CreateComponentEvent1)
                .Then(Constants.CreateComponentEvent2)
                .Then(Constants.CreateComponentEvent3)
                .Setup();

            var action = new Action(Constants.InitialProps);
            var evaluator = new Evaluator(contextMock.Object, historyMock.Object, action);

            var actual = evaluator.Evaluate();

            var expected = Constants.SucceedResult(action);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Evaluate_ComponentIncomplete_ReturnsFailed()
        {
            var contextMock = new Mock<IEvaluationContext>();
            SetupAllocateID(contextMock);
            SetupCreateComponentFailed(contextMock);

            var historyMock = new Mock<IHistory>();
            HistoryBuilder.New.UseMock(historyMock)
                .Then(Constants.AllocateIDEvent)
                .Then(Constants.RegisterPlayerEvent)
                .Then(Constants.CreateComponentEvent1)
                .Then(Constants.CreateComponentEvent2Failed)
                .Setup();

            var action = new Action(Constants.InitialProps);
            var evaluator = new Evaluator(contextMock.Object, historyMock.Object, action);

            var actual = evaluator.Evaluate();

            var expected = Constants.FailedResult(action);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
