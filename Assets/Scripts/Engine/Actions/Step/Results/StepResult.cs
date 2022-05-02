namespace RineaR.MadeHighlow.Actions
{
    /// <summary>
    ///     オブジェクトがフィールド上を歩いて1マス移動するアクションの結果
    /// </summary>
    public abstract record StepResult(StepResultCode Code) : Result(ActionType.Step);
}