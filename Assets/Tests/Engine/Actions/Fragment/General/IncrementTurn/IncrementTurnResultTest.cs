using NUnit.Framework;

namespace RineaR.MadeHighlow.Actions.IncrementTurn
{
    public class IncrementTurnResultTest
    {
        [Test]
        public void Simulate_Always_ReturnsUpdated()
        {
            var beforeWorld = WorldGenerator.Empty with { CurrentTurn = new Turn(1) };
            var result = new IncrementTurnResult(IncrementTurnActionGenerator.Empty, new Turn(2));

            var actual = result.Simulate(beforeWorld);

            var expected = beforeWorld with { CurrentTurn = new Turn(2) };
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
