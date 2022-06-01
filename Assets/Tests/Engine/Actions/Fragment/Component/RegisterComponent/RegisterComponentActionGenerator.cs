namespace RineaR.MadeHighlow.Actions.RegisterComponent
{
    public static class RegisterComponentActionGenerator
    {
        public static RegisterComponentAction Empty => new(
            AttachableIDGenerator.Empty,
            ID.None,
            ComponentGenerator.Empty
        );
    }
}
