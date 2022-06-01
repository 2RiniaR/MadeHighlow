using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     カード
    /// </summary>
    public abstract record Card(
        ID ID,
        [NotNull] PlayerID OwnerPlayerID,
        CardGenre Genre,
        Quickness Quickness,
        [NotNull] [ItemNotNull] ValueList<Component> Components
    ) : IIdentified, IAttachable
    {
        public CardID CardID => new(ID);

        public IAttachable WithComponents(ValueList<Component> components)
        {
            return this with { Components = components };
        }

        IAttachableID IAttachable.AttachableID => CardID;

        [NotNull]
        [ItemNotNull]
        public ValueList<IObject> GetChildren()
        {
            return ValueList.Concat(
                Components.Select(item => item as IObject),
                Components.SelectMany(item => item.GetChildren())
            );
        }
    }
}
