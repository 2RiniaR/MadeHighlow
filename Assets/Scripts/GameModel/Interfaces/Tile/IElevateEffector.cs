namespace RineaR.MadeHighlow.GameModel.Interfaces.Tile
{
    public interface IElevateEffector
    {
        public void OnElevate(GameModel.Tile tile, ref int? elevation);
    }
}