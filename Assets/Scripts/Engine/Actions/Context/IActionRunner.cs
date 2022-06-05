using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.CreateCard;

namespace RineaR.MadeHighlow.Actions
{
    public interface IActionRunner
    {
        [NotNull]
        ReactedResult<IValidResult> Run([NotNull] IHistory history, [NotNull] IValidAction action);

        [NotNull]
        Result CreateCard([NotNull] IHistory history, [NotNull] Action action);

        [NotNull]
        DeleteCard.Result DeleteCard([NotNull] IHistory history, [NotNull] DeleteCard.Action action);

        [NotNull]
        CreateComponent.Result CreateComponent([NotNull] IHistory history, [NotNull] CreateComponent.Action action);

        [NotNull]
        DeleteComponent.Result DeleteComponent([NotNull] IHistory history, [NotNull] DeleteComponent.Action action);

        [NotNull]
        CreateEntity.Result CreateEntity([NotNull] IHistory history, [NotNull] CreateEntity.Action action);

        [NotNull]
        DeleteEntity.Result DeleteEntity([NotNull] IHistory history, [NotNull] DeleteEntity.Action action);

        [NotNull]
        MoveEntity.Result MoveEntity([NotNull] IHistory history, [NotNull] MoveEntity.Action action);

        [NotNull]
        PositionEntity.Result PositionEntity([NotNull] IHistory history, [NotNull] PositionEntity.Action action);

        [NotNull]
        AllocateID.Result AllocateID([NotNull] IHistory history);

        [NotNull]
        IncrementTurn.Result IncrementTurn([NotNull] IHistory history);

        [NotNull]
        CreatePlayer.Result CreatePlayer([NotNull] IHistory history, [NotNull] CreatePlayer.Action action);

        [NotNull]
        CreateTile.Result CreateTile([NotNull] IHistory history, [NotNull] CreateTile.Action action);

        [NotNull]
        DeleteTile.Result DeleteTile([NotNull] IHistory history, [NotNull] DeleteTile.Action action);

        [NotNull]
        PositionTile.Result PositionTile([NotNull] IHistory history, [NotNull] PositionTile.Action action);

        [NotNull]
        ReactedResult<DropCard.Result> DropCard([NotNull] IHistory history, [NotNull] DropCard.Action action);

        [NotNull]
        ReactedResult<PayCard.Result> PayCard([NotNull] IHistory history, [NotNull] PayCard.Action action);

        [NotNull]
        ReactedResult<SupplyCard.Result> SupplyCard([NotNull] IHistory history, [NotNull] SupplyCard.Action action);

        [NotNull]
        ReactedResult<ReserveCommand.Result> ReserveCommand(
            [NotNull] IHistory history,
            [NotNull] ReserveCommand.Action action
        );

        [NotNull]
        ReactedResult<RunCommand.Result> RunCommand([NotNull] IHistory history, [NotNull] RunCommand.Action action);

        [NotNull]
        ReactedResult<StartCommands.Result> StartCommands(
            [NotNull] IHistory history,
            [NotNull] StartCommands.Action action
        );

        [NotNull]
        ReactedResult<AddComponent.Result> AddComponent(
            [NotNull] IHistory history,
            [NotNull] AddComponent.Action action
        );

        [NotNull]
        ReactedResult<RemoveComponent.Result> RemoveComponent(
            [NotNull] IHistory history,
            [NotNull] RemoveComponent.Action action
        );

        [NotNull]
        ReactedResult<DestroyEntity.Result> DestroyEntity(
            [NotNull] IHistory history,
            [NotNull] DestroyEntity.Action action
        );

        [NotNull]
        ReactedResult<EntityFly.Result> EntityFly([NotNull] IHistory history, [NotNull] EntityFly.Action action);

        [NotNull]
        ReactedResult<EntityStep.Result> EntityStep([NotNull] IHistory history, [NotNull] EntityStep.Action action);

        [NotNull]
        ReactedResult<EntityTeleport.Result> EntityTeleport(
            [NotNull] IHistory history,
            [NotNull] EntityTeleport.Action action
        );

        [NotNull]
        ReactedResult<EntityWalk.Result> EntityWalk([NotNull] IHistory history, [NotNull] EntityWalk.Action action);

        [NotNull]
        ReactedResult<GenerateEntity.Result> GenerateEntity(
            [NotNull] IHistory history,
            [NotNull] GenerateEntity.Action action
        );

        [NotNull]
        ReactedResult<InstantDamage.Result> InstantDamage(
            [NotNull] IHistory history,
            [NotNull] InstantDamage.Action action
        );

        [NotNull]
        ReactedResult<InstantDeath.Result> InstantDeath(
            [NotNull] IHistory history,
            [NotNull] InstantDeath.Action action
        );

        [NotNull]
        ReactedResult<InstantHeal.Result> InstantHeal([NotNull] IHistory history, [NotNull] InstantHeal.Action action);

        [NotNull]
        ReactedResult<InteractResult> Interact([NotNull] IHistory history, [NotNull] InteractAction action);

        [NotNull]
        ReactedResult<KnockBack.Result> KnockBack([NotNull] IHistory history, [NotNull] KnockBack.Action action);

        [NotNull]
        ReactedResult<BigBang.Result> BigBang([NotNull] IHistory history, [NotNull] BigBang.Action action);

        [NotNull]
        ReactedResult<UpdateTurn.Result> UpdateTurn([NotNull] IHistory history, [NotNull] UpdateTurn.Action action);

        [NotNull]
        ReactedResult<JoinPlayer.Result> JoinPlayer([NotNull] IHistory history, [NotNull] JoinPlayer.Action action);

        [NotNull]
        ReactedResult<DestroyTile.Result> DestroyTile([NotNull] IHistory history, [NotNull] DestroyTile.Action action);

        [NotNull]
        ReactedResult<ElevateTile.Result> ElevateTile([NotNull] IHistory history, [NotNull] ElevateTile.Action action);

        [NotNull]
        ReactedResult<GenerateTile.Result> GenerateTile(
            [NotNull] IHistory history,
            [NotNull] GenerateTile.Action action
        );
    }
}
