using Moq;
using NUnit.Framework;
using RineaR.MadeHighlow.Actions.EvaluationFlows.Rejection;

namespace RineaR.MadeHighlow.Actions.CreateComponent
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

        private static void SetupNoRejection(Mock<IEvaluationContext> contextMock)
        {
            contextMock.Setup(
                context => context.Flows.CheckRejection(
                    It.IsAny<IHistory>(),
                    It.IsAny<ContextProvider<RejectionContext>>(),
                    It.IsAny<RejectHandler>()
                )
            );
        }

        private static void SetupParentExist(Mock<IEvaluationContext> contextMock)
        {
            contextMock.Setup(context => context.Finder.FindAttachable(It.IsAny<World>(), Constants.Parent.PlayerID))
                .Returns(Constants.Parent);
        }

        private static void SetupParentNotExist(Mock<IEvaluationContext> contextMock)
        {
            contextMock.Setup(context => context.Finder.FindAttachable(It.IsAny<World>(), Constants.Parent.PlayerID))
                .Returns(null as IAttachable);
        }

        private static void SetupRejection(Mock<IEvaluationContext> contextMock, Rejection rejection)
        {
            contextMock.Setup(
                    context => context.Flows.CheckRejection(
                        It.IsAny<IHistory>(),
                        It.IsAny<ContextProvider<RejectionContext>>(),
                        It.IsAny<RejectHandler>()
                    )
                )
                .Callback(
                    (IHistory _, ContextProvider<RejectionContext> _, RejectHandler rejectHandler) =>
                        rejectHandler(rejection)
                );
        }

        [Test]
        public void Evaluate_Valid_ReturnsSucceed()
        {
            var contextMock = new Mock<IEvaluationContext>();
            SetupParentExist(contextMock);
            SetupNoRejection(contextMock);
            var context = contextMock.Object;

            var initialHistoryMock = new Mock<IHistory>();
            SetupAllocateID(contextMock, initialHistoryMock, Mock.Of<IHistory>());
            var initialHistory = initialHistoryMock.Object;

            var action = new Action(Constants.Parent.PlayerID, Constants.InitialProps);
            var evaluator = new Evaluator(context, initialHistory, action);

            var actual = evaluator.Evaluate();

            var expected = Constants.SucceedResult(action);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Evaluate_Rejected_ReturnsFailed()
        {
            var contextMock = new Mock<IEvaluationContext>();
            SetupParentExist(contextMock);
            SetupRejection(contextMock, Constants.Rejection);
            var context = contextMock.Object;

            var initialHistoryMock = new Mock<IHistory>();
            SetupAllocateID(contextMock, initialHistoryMock, Mock.Of<IHistory>());
            var initialHistory = initialHistoryMock.Object;

            var action = new Action(Constants.Parent.PlayerID, Constants.InitialProps);
            var evaluator = new Evaluator(context, initialHistory, action);

            var actual = evaluator.Evaluate();

            var expected = Constants.RejectedResult(action);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Evaluate_NoParent_ReturnsFailed()
        {
            var contextMock = new Mock<IEvaluationContext>();
            SetupParentNotExist(contextMock);
            SetupNoRejection(contextMock);
            var context = contextMock.Object;

            var initialHistoryMock = new Mock<IHistory>();
            SetupAllocateID(contextMock, initialHistoryMock, Mock.Of<IHistory>());
            var initialHistory = initialHistoryMock.Object;

            var action = new Action(Constants.Parent.PlayerID, Constants.InitialProps);
            var evaluator = new Evaluator(context, initialHistory, action);

            var actual = evaluator.Evaluate();

            var expected = Constants.FailedResult(action);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
