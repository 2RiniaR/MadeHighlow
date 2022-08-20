using RineaR.MadeHighlow.Clients.Local.Strategy.Tools;
using RineaR.MadeHighlow.GameModel;

namespace RineaR.MadeHighlow.GameData.CardEffects.Options
{
    public interface IWalkRouteOption
    {
        Command Activate(WalkRoutePrediction routePrediction);
    }
}