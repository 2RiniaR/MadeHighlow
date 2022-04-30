using JetBrains.Annotations;
using NUnit.Framework;

namespace RineaR.MadeHighlow.Actions.BigBang
{
    public class SucceedBigBangEventTest
    {
        [NotNull]
        private static World CreateDefaultWorld()
        {
            return new World
            {
                Players = new ValueObjectList<Player>(
                    new Player { ID = ID<Player>.From(1) },
                    new Player { ID = ID<Player>.From(2) },
                    new Player { ID = ID<Player>.From(3) },
                    new Player { ID = ID<Player>.From(4) }
                ),
                Objects = new ValueObjectList<Object>(
                    new Tile { ID = ID<Object>.From(1) },
                    new Tile { ID = ID<Object>.From(2) },
                    new Tile { ID = ID<Object>.From(3) }
                ),
                CurrentTurn = new Turn(),
            };
        }

        [NotNull]
        private static SucceedBigBangEvent CreateDefaultEvent()
        {
            return new SucceedBigBangEvent { GeneratedWorld = CreateDefaultWorld() };
        }

        [Test]
        public void Run_Always_ReturnsSameWorld()
        {
            var @event = CreateDefaultEvent();

            var actual = @event.Simulate(new World());

            Assert.That(actual, Is.EqualTo(CreateDefaultWorld()));
        }
    }
}