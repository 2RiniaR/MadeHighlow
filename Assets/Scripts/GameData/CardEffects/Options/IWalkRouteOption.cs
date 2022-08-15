using RineaR.MadeHighlow.GameModel;

namespace RineaR.MadeHighlow.GameData.CardEffects.Options
{
    public interface IWalkRouteOption
    {
        Command Activate(WalkRoute route);
    }
}