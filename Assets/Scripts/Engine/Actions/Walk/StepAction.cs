namespace RineaR.MadeHighlow.Actions.Walk
{
    /// <summary>
    ///     1マスの歩行の追加効果
    /// </summary>
    /// <param name="Type">種類</param>
    public record StepAction(in StepActionType Type);
}