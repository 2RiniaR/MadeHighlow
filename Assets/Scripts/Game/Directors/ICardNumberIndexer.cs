using Game.Entities;
using Game.Primitives;

namespace Game.Directors
{
    public interface ICardNumberIndexer
    {
        public ICardNumber GetByID(CardNumberID id);
    }
}