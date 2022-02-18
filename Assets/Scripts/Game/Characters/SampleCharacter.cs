using Game.Entities;
using Game.Primitives;

namespace Game.Characters
{
    public class SampleCharacter : ICharacter
    {
        public CharacterID ID { get; }

        public SampleCharacter(CharacterID id)
        {
            ID = id;
        }

        public IUnit GenerateUnit()
        {
            throw new System.NotImplementedException();
        }
    }
}