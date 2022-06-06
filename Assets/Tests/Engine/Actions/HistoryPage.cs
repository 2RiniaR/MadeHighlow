using JetBrains.Annotations;
using Moq;

namespace RineaR.MadeHighlow.Actions
{
    public delegate void AppendedCallback<TResult>([NotNull] TResult _, [NotNull] out Event<TResult> @event)
        where TResult : IResult;

    public static class HistoryBuilder
    {
        [NotNull] public static IHistorySetup New => new HistoryStart();
    }

    public interface IHistorySetup
    {
        [CanBeNull] public Mock<IHistory> HistoryMock { get; set; }
        [CanBeNull] public World World { get; set; }

        [NotNull]
        IHistorySetup UseMock([NotNull] Mock<IHistory> historyMock);

        [NotNull]
        IHistorySetup SetWorld([NotNull] World world);

        [NotNull]
        public IHistorySetup Then<TNext>([NotNull] Event<TNext> lastEvent) where TNext : class, IResult;

        void SetupInternal<TNext>(IHistory nextHistory, Event<TNext> nextEvent) where TNext : class, IResult;

        void Setup([CanBeNull] IHistory nextHistory = null);
    }

    public class HistoryStart : IHistorySetup
    {
        public Mock<IHistory> HistoryMock { get; set; }
        public World World { get; set; }

        public IHistorySetup Then<TNext>(Event<TNext> lastEvent) where TNext : class, IResult
        {
            return new HistoryPage<TNext>(this, lastEvent);
        }

        public IHistorySetup UseMock(Mock<IHistory> historyMock)
        {
            HistoryMock = historyMock;
            return this;
        }

        public IHistorySetup SetWorld(World world)
        {
            World = world;
            return this;
        }

        public void SetupInternal<TNext>(IHistory nextHistory, Event<TNext> nextEvent) where TNext : class, IResult
        {
            HistoryMock ??= new Mock<IHistory>();

            if (World != null)
            {
                HistoryMock.SetupGet(history => history.World).Returns(World);
            }

            HistoryMock.Setup(history => history.Appended(It.IsAny<TNext>(), out It.Ref<Event<TNext>>.IsAny))
                .Returns(nextHistory ?? Mock.Of<IHistory>())
                .Callback(new AppendedCallback<TNext>((TNext _, out Event<TNext> @event) => @event = nextEvent));
        }

        public void Setup(IHistory nextHistory = null)
        {
            HistoryMock ??= new Mock<IHistory>();

            if (World != null)
            {
                HistoryMock.SetupGet(history => history.World).Returns(World);
            }
        }
    }

    public class HistoryPage<TResult> : IHistorySetup where TResult : class, IResult
    {
        public HistoryPage([NotNull] IHistorySetup beforePage, [NotNull] Event<TResult> lastEvent)
        {
            BeforePage = beforePage;
            LastEvent = lastEvent;
        }

        [NotNull] private IHistorySetup BeforePage { get; }
        [NotNull] public Event<TResult> LastEvent { get; }

        public Mock<IHistory> HistoryMock { get; set; }
        public World World { get; set; }

        public IHistorySetup Then<TNext>(Event<TNext> lastEvent) where TNext : class, IResult
        {
            return new HistoryPage<TNext>(this, lastEvent);
        }

        public void SetupInternal<TNext>(IHistory nextHistory, Event<TNext> nextEvent) where TNext : class, IResult
        {
            HistoryMock ??= new Mock<IHistory>();

            if (World != null)
            {
                HistoryMock.SetupGet(history => history.World).Returns(World);
            }

            HistoryMock.Setup(history => history.Appended(It.IsAny<TNext>(), out It.Ref<Event<TNext>>.IsAny))
                .Returns(nextHistory ?? Mock.Of<IHistory>())
                .Callback(new AppendedCallback<TNext>((TNext _, out Event<TNext> @event) => @event = nextEvent));

            BeforePage.SetupInternal(HistoryMock.Object, LastEvent);
        }

        public void Setup(IHistory nextHistory = null)
        {
            HistoryMock ??= new Mock<IHistory>();

            if (World != null)
            {
                HistoryMock.SetupGet(history => history.World).Returns(World);
            }

            BeforePage.SetupInternal(HistoryMock.Object, LastEvent);
        }

        public IHistorySetup UseMock(Mock<IHistory> historyMock)
        {
            HistoryMock = historyMock;
            return this;
        }

        public IHistorySetup SetWorld(World world)
        {
            World = world;
            return this;
        }
    }
}
