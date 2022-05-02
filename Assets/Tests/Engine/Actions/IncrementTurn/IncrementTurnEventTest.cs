using NUnit.Framework;

namespace RineaR.MadeHighlow.Actions.IncrementTurn
{
    public class IncrementTurnEventTest
    {
        #region Simulate

        [Test]
        public void Simulate_Always_ReturnsTurnIncremented()
        {
            var @event = new IncrementTurnResult();
            var world = new World { CurrentTurn = new Turn() };

            var actual = @event.Simulate(world);

            Assert.That(actual.CurrentTurn, Is.EqualTo(world.CurrentTurn.Increment()));
        }

        [Test]
        public void Simulate_Always_ReturnsAllComponentsDurationDecremented()
        {
            var @event = new IncrementTurnResult();
            var duration = new TurnDuration(1);
            var component = new EmptyComponent { Duration = duration };
            var @object = new Entity { Components = new ValueObjectList<Component>(component, component) };
            var world = new World { Objects = new ValueObjectList<Object>(@object, @object) };

            var actual = @event.Simulate(world);
            var actualDurations = new[]
            {
                actual.Objects.Items[0].Components.Items[0].Duration,
                actual.Objects.Items[0].Components.Items[1].Duration,
                actual.Objects.Items[1].Components.Items[0].Duration,
                actual.Objects.Items[1].Components.Items[1].Duration,
            };

            var expectedDuration = duration.Decrement();
            var expectedComponent = new EmptyComponent { Duration = expectedDuration };
            var expectedObject = new Entity
                { Components = new ValueObjectList<Component>(expectedComponent, expectedComponent) };
            var expected = new World { Objects = new ValueObjectList<Object>(expectedObject, expectedObject) };
            var expectedDurations = new[]
            {
                expected.Objects.Items[0].Components.Items[0].Duration,
                expected.Objects.Items[0].Components.Items[1].Duration,
                expected.Objects.Items[1].Components.Items[0].Duration,
                expected.Objects.Items[1].Components.Items[1].Duration,
            };
            Assert.That(actualDurations, Is.EquivalentTo(expectedDurations));
        }

        #endregion
    }
}