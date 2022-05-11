using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     エンティティを新規登録するアクション
    /// </summary>
    public record RegisterEntityAction([NotNull] Entity InitialEntity) : Action<RegisterEntityResult>
    {
        public override RegisterEntityResult Validate(IActionContext context)
        {
            var allocateIDResult = new AllocateIDAction().Validate(context);
            var formattedEntity = InitialEntity with
            {
                ID = allocateIDResult.AllocatedID,
                Components = ValueList<Component>.Empty,
            };

            return new SucceedRegisterEntityResult(allocateIDResult, formattedEntity);
        }
    }
}
