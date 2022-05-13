using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateEntity.RegisterEntity
{
    public record RegisterEntityAction([NotNull] Entity InitialProps) : Action<RegisterEntityResult>
    {
        public override RegisterEntityResult Evaluate(IActionContext context)
        {
            var allocateIDResult = new AllocateIDAction().Evaluate(context);
            var formattedEntity = InitialProps with
            {
                ID = allocateIDResult.AllocatedID,
                Components = ValueList<Component>.Empty,
            };

            return new SucceedResult(allocateIDResult, formattedEntity);
        }
    }
}
