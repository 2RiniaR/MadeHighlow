using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public interface IInitializableComponent
    {
        public InitializeComponentResult OnInitialize([NotNull] ISessionModel session);
    }
}