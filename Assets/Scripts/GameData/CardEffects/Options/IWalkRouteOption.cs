using RineaR.MadeHighlow.Clients.Local.Field;
using RineaR.MadeHighlow.GameModel;

namespace RineaR.MadeHighlow.GameData.CardEffects.Options
{
    public interface IWalkRouteOption
    {
        Command Activate(WalkRoutePrediction routePrediction);
    }
}