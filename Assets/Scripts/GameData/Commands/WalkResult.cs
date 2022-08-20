using RineaR.MadeHighlow.GameData.CardEffects;
using RineaR.MadeHighlow.GameModel;

namespace RineaR.MadeHighlow.GameData.Commands
{
    public class WalkResult : ICommandResult
    {
        public Figure Walker { get; init; }
        public WalkRoute Route { get; init; }
    }
}