using NUnit.Framework;

namespace RineaR.MadeHighlow.Actions.AllocateID
{
    public class AllocateIDResultTest
    {
        [Test]
        public void Simulate_Always_ReturnsUpdated()
        {
            var beforeWorld = WorldGenerator.Empty with { LatestAllocatedID = ID.From(1) };
            var result = new AllocateIDResult(AllocateIDActionGenerator.Empty, ID.From(2));

            var actual = result.Simulate(beforeWorld);

            var expected = beforeWorld with { LatestAllocatedID = ID.From(2) };
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
