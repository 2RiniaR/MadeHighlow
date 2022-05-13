using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.AddComponent.RegisterComponent
{
    public record RegisterComponentAction([NotNull] IAttachableID ParentID, [NotNull] Component InitialProps)
    {
        public RegisterComponentResult Evaluate(IActionContext context)
        {
            var parent = ParentID.GetFrom(context.World);
            if (parent == null)
            {
                return new ParentNotFoundResult(ParentID);
            }

            var allocateIDResult = new AllocateIDAction().Evaluate(context);
            var formatted = InitialProps with
            {
                ID = allocateIDResult.AllocatedID,
                AttachedID = ParentID,
            };

            return new SucceedResult(allocateIDResult, formatted);
        }
    }
}
