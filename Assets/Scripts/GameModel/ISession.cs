namespace RineaR.MadeHighlow.GameModel
{
    public interface ISession
    {
        int Turn { get; }
        Field Field { get; }
        CommandStack CommandStack { get; }
        PlayerContainer Players { get; }
    }
}