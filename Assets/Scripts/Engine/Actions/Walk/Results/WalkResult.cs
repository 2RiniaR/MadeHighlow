namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     オブジェクトがフィールド上を歩いて移動するアクションの結果
    /// </summary>
    public record WalkResult : Result
    {
        /// <summary>
        ///     行動したユニット
        /// </summary>
        public EntityEnsuredID Actor { get; init; } = new();

        /// <summary>
        ///     ステップ
        /// </summary>
        public ValueObjectList<StepResult> Steps { get; init; } = ValueObjectList<StepResult>.Empty;

        public override World Simulate(in World world)
        {
            return Steps.Aggregate(world, (currentWorld, step) => step.Simulate(currentWorld));
        }
    }
}