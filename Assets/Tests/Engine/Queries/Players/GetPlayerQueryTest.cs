using System;
using JetBrains.Annotations;
using NUnit.Framework;

namespace RineaR.MadeHighlow.Queries.Players
{
    public class GetPlayerQueryTest
    {
        [NotNull]
        private static World CreateDefaultWorld()
        {
            return new World
            {
                Players = new ValueObjectList<Player>(
                    new Player { ID = ID<Player>.From(1) },
                    new Player { ID = ID<Player>.From(3) },
                    new Player { ID = ID<Player>.From(4) },
                    new Player { ID = ID<Player>.From(5) }
                ),
            };
        }

        private static GetPlayerQuery CreateQuery(uint id)
        {
            return new GetPlayerQuery
            {
                Locator = new PlayerLocator { PlayerID = ID<Player>.From(id) },
            };
        }


        [Test]
        public void Run_Exist_ReturnsItem()
        {
            var world = CreateDefaultWorld();
            var query = CreateQuery(1);

            var actual = query.Run(world);

            Assert.That(actual.ID, Is.EqualTo(ID<Player>.From(1)));
        }

        [Test]
        public void Run_NotExist_ThrowsNullReferenceException()
        {
            var world = CreateDefaultWorld();
            var query = CreateQuery(2);

            Assert.That(() => query.Run(world), Throws.TypeOf<NullReferenceException>());
        }
    }
}