using Moq;
using NUnit.Framework;
using RineaR.MadeHighlow.Actions.EvaluationFlows.Rejection;

namespace RineaR.MadeHighlow.Actions.CreateComponent
{
    public class CreateComponentEvaluatorTest
    {
        private static void SetupAllocateID(
            Mock<IEvaluationContext> contextMock,
            Mock<IHistory> historyMock,
            IHistory nextHistory
        )
        {
            contextMock.Setup(context => context.Actions.AllocateID(It.IsAny<IHistory>())).Returns(AllocateID.Content);
            historyMock.SetupNextEvent(AllocateID, nextHistory);
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
            contextMock.Setup(context => context.Finder.FindAttachable(It.IsAny<World>(), Parent.PlayerID))
                .Returns(Parent);
        }

        private static void SetupParentNotExist(Mock<IEvaluationContext> contextMock)
        {
            contextMock.Setup(context => context.Finder.FindAttachable(It.IsAny<World>(), Parent.PlayerID))
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

        private static Player Parent { get; } = PlayerGenerator.Empty with { ID = ID.From(1) };

        private static Component InitialProps { get; } = ComponentGenerator.Empty;
        private static ID ComponentID { get; } = ID.From(2);

        private static Component RejectedComponent { get; } = ComponentGenerator.Empty with { ID = ID.From(3) };
        private static Rejection Rejection { get; } = new(RejectedComponent.ComponentID);

        private static EventID BeforeEventID { get; } = new(ID.From(1));
        private static EventID AllocateIDEventID { get; } = new(ID.From(2));

        private static Event<AllocateID.Result> AllocateID { get; } = new(
            AllocateIDEventID,
            BeforeEventID,
            new AllocateID.Result(ComponentID)
        );

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

            var action = new Action(Parent.PlayerID, InitialProps);
            var evaluator = new Evaluator(context, initialHistory, action);

            var actual = evaluator.Evaluate();

            var expected = new Result(action)
            {
                AllocateID = AllocateID,
                Rejection = null,
                Created = InitialProps with { ID = ComponentID, AttachedID = Parent.PlayerID },
            };
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Evaluate_Rejected_ReturnsFailed()
        {
            var contextMock = new Mock<IEvaluationContext>();
            SetupParentExist(contextMock);
            SetupRejection(contextMock, Rejection);
            var context = contextMock.Object;

            var initialHistoryMock = new Mock<IHistory>();
            SetupAllocateID(contextMock, initialHistoryMock, Mock.Of<IHistory>());
            var initialHistory = initialHistoryMock.Object;

            var action = new Action(Parent.PlayerID, InitialProps);
            var evaluator = new Evaluator(context, initialHistory, action);

            var actual = evaluator.Evaluate();

            var expected = new Result(action)
            {
                AllocateID = AllocateID,
                Rejection = Rejection,
                Created = null,
            };
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Evaluate_RegisterFailed_ReturnsFailed()
        {
            var contextMock = new Mock<IEvaluationContext>();
            SetupParentNotExist(contextMock);
            SetupNoRejection(contextMock);
            var context = contextMock.Object;

            var initialHistoryMock = new Mock<IHistory>();
            SetupAllocateID(contextMock, initialHistoryMock, Mock.Of<IHistory>());
            var initialHistory = initialHistoryMock.Object;

            var action = new Action(Parent.PlayerID, InitialProps);
            var evaluator = new Evaluator(context, initialHistory, action);

            var actual = evaluator.Evaluate();

            var expected = new Result(action)
            {
                AllocateID = null,
                Rejection = null,
                Created = null,
            };
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
