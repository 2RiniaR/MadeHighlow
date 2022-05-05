using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface IInitializableComponent
    {
        public InitializeComponentResult OnInitialize([NotNull] IActionContext session);
    }
}