using System.Collections.Generic;
using Game.CardNumbers;
using Game.Entities;
using Game.Primitives;

namespace Game.Constants
{
    public static class CardNumberIndexer
    {
        public static readonly IReadOnlyDictionary<CardNumberID, ICardNumber> Indices =
            new Dictionary<CardNumberID, ICardNumber>
            {
                { CardNumberID.FromIdentity(0), new SampleCardNumber() }
            };
    }
}