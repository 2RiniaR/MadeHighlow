using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Engine.Subjects.Cards.Commands
{
    /// <summary>
    ///     「カードの指令」の種類
    /// </summary>
    public record CommandType([NotNull] in string Name);
}