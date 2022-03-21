using JetBrains.Annotations;
using NUnit.Framework;
using RineaR.MadeHighlow.Engine.Subjects;
using RineaR.MadeHighlow.Engine.Subjects.Objects;
using RineaR.MadeHighlow.Engine.Subjects.Objects.Tiles;
using RineaR.MadeHighlow.Engine.Subjects.Players;

namespace RineaR.MadeHighlow.Engine.Events.BigBang
{
    public class BigBangEventTest
    {
        [NotNull]
        private static World CreateDefaultWorld()
        {
            return new World
            {
                Players =
                {
                    new Player { ID = ID<Player>.From(0) },
                    new Player { ID = ID<Player>.From(1) },
                    new Player { ID = ID<Player>.From(2) },
                    new Player { ID = ID<Player>.From(3) },
                },
                Objects =
                {
                    new Tile { ID = ID<Object>.From(0) },
                    new Tile { ID = ID<Object>.From(1) },
                    new Tile { ID = ID<Object>.From(2) },
                },
                CurrentTurn = new Turn(),
            };
        }

        [NotNull]
        private static BigBangEvent CreateDefaultEvent()
        {
            return new BigBangEvent { GeneratedWorld = CreateDefaultWorld() };
        }

        [Test]
        public void Run_Value_ReturnsGeneratedWorld()
        {
            var @event = CreateDefaultEvent();

            var actual = @event.Simulate(new World());

            Assert.That(actual, Is.EqualTo(CreateDefaultWorld()));
        }
    }
}