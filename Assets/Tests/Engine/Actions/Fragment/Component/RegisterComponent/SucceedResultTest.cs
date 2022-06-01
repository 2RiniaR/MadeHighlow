using NUnit.Framework;

namespace RineaR.MadeHighlow.Actions.RegisterComponent
{
    public class SucceedResultTest
    {
        [Test]
        public void Simulate_Always_ReturnsUpdated()
        {
            var beforePlayer = PlayerGenerator.Empty with { ID = ID.From(1) };
            var registeredComponent = ComponentGenerator.Empty with
            {
                ID = ID.From(2), AttachedID = beforePlayer.PlayerID,
            };
            var beforeWorld = WorldGenerator.Empty with { Players = new ValueList<Player>(beforePlayer) };
            var result = new SucceedResult(RegisterComponentActionGenerator.Empty, registeredComponent);

            var actual = result.Simulate(beforeWorld);

            var afterPlayer = beforePlayer with { Components = new ValueList<Component>(registeredComponent) };
            var expected = beforeWorld with { Players = new ValueList<Player>(afterPlayer) };
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
