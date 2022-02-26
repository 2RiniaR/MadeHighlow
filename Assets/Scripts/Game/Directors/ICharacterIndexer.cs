using Game.Entities;
using Game.Primitives;

namespace Game.Directors
{
    public interface ICharacterIndexer
    {
        public ICharacter GetByID(CharacterID id);
    }
}