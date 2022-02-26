using System.Collections.Generic;
using Game.Characters;
using Game.Entities;
using Game.Primitives;

namespace Game.Constants
{
    public static class CharacterIndexer
    {
        public static readonly IReadOnlyDictionary<CharacterID, ICharacter> Indices =
            new Dictionary<CharacterID, ICharacter>
            {
                { CharacterID.FromIdentity(0), new SampleCharacter(CharacterID.FromIdentity(0)) }
            };
    }
}