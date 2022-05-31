namespace RineaR.MadeHighlow
{
    public static class PlayerGenerator
    {
        public static Player Empty => new(ID.None, ValueList<Card>.Empty, new DeckSize(0), ValueList<Component>.Empty);
    }
}
