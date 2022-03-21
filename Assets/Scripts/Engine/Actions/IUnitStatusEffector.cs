using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Subjects;
using RineaR.MadeHighlow.Engine.Subjects.Objects;
using RineaR.MadeHighlow.Engine.Subjects.Objects.Units;

namespace RineaR.MadeHighlow.Engine.Actions
{
    public interface IUnitStatusEffector
    {
        [NotNull]
        public Unit OnReferenced([NotNull] in World world, [NotNull] in ObjectLocator locator);
    }
}