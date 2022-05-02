namespace RineaR.MadeHighlow.Actions
{
    /// <summary>
    ///     「行動」の種別
    /// </summary>
    public enum ActionType
    {
        Empty,
        AddComponent,
        RemoveComponent,
        BigBang,
        CommandUnit,
        Destroy,
        Generate,
        Interact,
        SupplyCard,
        IncrementTurn,
        Walk,
        Step,
        Teleport,
        ActuateUnit,
        Death,
        KnockBack,
        Damage,
        Heal,
    }
}