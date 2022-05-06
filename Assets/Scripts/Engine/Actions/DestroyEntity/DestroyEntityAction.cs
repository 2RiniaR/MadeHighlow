using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record DestroyEntityAction([NotNull] in EntityID TargetEntityID) : Action<DestroyEntityResult>
    {
        public override DestroyEntityResult Validate(in IActionContext context)
        {
            return new DestroyEntityResult(TargetEntityID);
        }
    }
}