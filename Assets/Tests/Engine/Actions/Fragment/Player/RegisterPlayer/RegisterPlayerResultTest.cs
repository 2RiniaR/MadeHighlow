using NUnit.Framework;

namespace RineaR.MadeHighlow.Actions.RegisterPlayer
{
    public class RegisterPlayerResultTest
    {
        [Test]
        public void Simulate_Always_ReturnsUpdated()
        {
            var beforeWorld = WorldGenerator.Empty with { Players = ValueList<Player>.Empty };
            var registeredPlayer = PlayerGenerator.Empty with { ID = ID.From(1) };
            var result = new RegisterPlayerResult(RegisterPlayerActionGenerator.Empty, registeredPlayer);

            var actual = result.Simulate(beforeWorld);

            var expected = beforeWorld with { Players = new ValueList<Player>(registeredPlayer) };
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
