using System.Collections.Immutable;
using JetBrains.Annotations;
using NUnit.Framework;
using RineaR.MadeHighlow.Engine.Exceptions;
using RineaR.MadeHighlow.Engine.Subjects;
using RineaR.MadeHighlow.Engine.Subjects.Players;

namespace RineaR.MadeHighlow.Engine.Queries.Players
{
    public class GetPlayerQueryTest
    {
        [NotNull]
        private static World CreateDefaultWorld()
        {
            return new World
            {
                Players = ImmutableList.Create(
                    new Player { ID = ID<Player>.From(1) },
                    new Player { ID = ID<Player>.From(3) },
                    new Player { ID = ID<Player>.From(4) },
                    new Player { ID = ID<Player>.From(5) }
                ),
            };
        }

        private static GetPlayerQuery CreateQuery(int id)
        {
            return new GetPlayerQuery
            {
                Locator = new PlayerLocator { PlayerID = ID<Player>.From(id) },
            };
        }


        [Test]
        [TestCase(1)]
        [TestCase(4)]
        public void Run_Exist_ReturnsItem(int id)
        {
            var world = CreateDefaultWorld();
            var query = CreateQuery(id);

            var actual = query.Run(world);

            Assert.That(actual.ID, Is.EqualTo(ID<Player>.From(id)));
        }

        [Test]
        [TestCase(2)]
        [TestCase(-5)]
        public void Run_NotExist_ThrowsNotExistException(int id)
        {
            var world = CreateDefaultWorld();
            var query = CreateQuery(id);

            Assert.That(() => query.Run(world), Throws.TypeOf<NotExistException>());
        }
    }
}