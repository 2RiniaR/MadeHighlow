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

        public record SampleEntityComponent : EntityComponent;

        [Test]
        public void Simulate_Always_ReturnsAllComponentsDurationDecremented()
        {
            var @event = new IncrementTurnResult();
            var duration = new TurnDuration(1);
            var component = new SampleEntityComponent { Duration = duration };
            var @object = new Entity { Components = new ValueObjectList<EntityComponent>(component, component) };
            var world = new World { Entities = new ValueObjectList<Entity>(@object, @object) };

            var actual = @event.Simulate(world);
            var actualDurations = new[]
            {
                actual.Entities[0].Components[0].Duration,
                actual.Entities[0].Components[1].Duration,
                actual.Entities[1].Components[0].Duration,
                actual.Entities[1].Components[1].Duration,
            };

            var expectedDuration = duration.Decrement();
            var expectedComponent = new SampleEntityComponent { Duration = expectedDuration };
            var expectedObject = new Entity
                { Components = new ValueObjectList<EntityComponent>(expectedComponent, expectedComponent) };
            var expected = new World { Entities = new ValueObjectList<Entity>(expectedObject, expectedObject) };
            var expectedDurations = new[]
            {
                expected.Entities[0].Components[0].Duration,
                expected.Entities[0].Components[1].Duration,
                expected.Entities[1].Components[0].Duration,
                expected.Entities[1].Components[1].Duration,
            };
            Assert.That(actualDurations, Is.EquivalentTo(expectedDurations));
        }

        #endregion
    }
}