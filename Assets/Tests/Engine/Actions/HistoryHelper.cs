using JetBrains.Annotations;
using Moq;

namespace RineaR.MadeHighlow.Actions
{
    public static class HistoryHelper
    {
        private delegate void AppendedCallback<TResult>([NotNull] TResult _, [NotNull] out Event<TResult> @event)
            where TResult : IResult;

        public static void SetupWorld([NotNull] this Mock<IHistory> mock, [NotNull] World world)
        {
            mock.SetupGet(history => history.World).Returns(world);
        }

        public static void SetupNextEvent<TResult>(
            [NotNull] this Mock<IHistory> mock,
            [NotNull] Event<TResult> returnEvent,
            [NotNull] IHistory nextHistory
        ) where TResult : IResult
        {
            mock.Setup(history => history.Appended(It.IsAny<TResult>(), out It.Ref<Event<TResult>>.IsAny))
                .Returns(nextHistory)
                .Callback(
                    new AppendedCallback<TResult>((TResult _, out Event<TResult> @event) => @event = returnEvent)
                );
        }
    }
}
