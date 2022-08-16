using RineaR.MadeHighlow.GameData.CardEffects;
using RineaR.MadeHighlow.GameModel;

namespace RineaR.MadeHighlow.GameData.Commands
{
    public class WalkResult : ICommandResult
    {
        public WalkRoute Route { get; init; }
    }
}