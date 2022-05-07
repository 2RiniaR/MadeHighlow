using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record DestroyEntityAction([NotNull] EntityID TargetEntityID) : Action<DestroyEntityResult>
    {
        public override DestroyEntityResult Validate(IActionContext context)
        {
            return new DestroyEntityResult(TargetEntityID);
        }
    }
}
