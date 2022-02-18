using Game.Primitives;

namespace Game.Entities
{
    public interface ICharacter
    {
        public CharacterID ID { get; }
        public IUnit GenerateUnit();
    }
}