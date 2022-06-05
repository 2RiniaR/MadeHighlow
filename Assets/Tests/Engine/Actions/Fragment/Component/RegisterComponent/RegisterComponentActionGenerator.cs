using RineaR.MadeHighlow.Actions.CreateComponent.RegisterComponent;

namespace RineaR.MadeHighlow.Actions.RegisterComponent
{
    public static class RegisterComponentActionGenerator
    {
        public static Action Empty => new(AttachableIDGenerator.Empty, ID.None, ComponentGenerator.Empty);
    }
}
