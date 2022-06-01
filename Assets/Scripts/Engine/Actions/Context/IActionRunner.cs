using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.AddComponent;
using RineaR.MadeHighlow.Actions.AllocateID;
using RineaR.MadeHighlow.Actions.CreateCard;
using RineaR.MadeHighlow.Actions.CreateComponent;
using RineaR.MadeHighlow.Actions.CreateEntity;
using RineaR.MadeHighlow.Actions.CreatePlayer;
using RineaR.MadeHighlow.Actions.CreateTile;
using RineaR.MadeHighlow.Actions.DeleteCard;
using RineaR.MadeHighlow.Actions.DeleteComponent;
using RineaR.MadeHighlow.Actions.DeleteEntity;
using RineaR.MadeHighlow.Actions.DeleteTile;
using RineaR.MadeHighlow.Actions.DestroyEntity;
using RineaR.MadeHighlow.Actions.DestroyTile;
using RineaR.MadeHighlow.Actions.DropCard;
using RineaR.MadeHighlow.Actions.ElevateTile;
using RineaR.MadeHighlow.Actions.EntityFly;
using RineaR.MadeHighlow.Actions.EntityStep;
using RineaR.MadeHighlow.Actions.EntityTeleport;
using RineaR.MadeHighlow.Actions.EntityWalk;
using RineaR.MadeHighlow.Actions.General.BigBang;
using RineaR.MadeHighlow.Actions.General.UpdateTurn;
using RineaR.MadeHighlow.Actions.GenerateEntity;
using RineaR.MadeHighlow.Actions.GenerateTile;
using RineaR.MadeHighlow.Actions.IncrementTurn;
using RineaR.MadeHighlow.Actions.InstantDamage;
using RineaR.MadeHighlow.Actions.InstantDeath;
using RineaR.MadeHighlow.Actions.InstantHeal;
using RineaR.MadeHighlow.Actions.JoinPlayer;
using RineaR.MadeHighlow.Actions.KnockBack;
using RineaR.MadeHighlow.Actions.MoveEntity;
using RineaR.MadeHighlow.Actions.PayCard;
using RineaR.MadeHighlow.Actions.PlaceCard;
using RineaR.MadeHighlow.Actions.PositionEntity;
using RineaR.MadeHighlow.Actions.PositionTile;
using RineaR.MadeHighlow.Actions.RegisterCard;
using RineaR.MadeHighlow.Actions.RegisterComponent;
using RineaR.MadeHighlow.Actions.RegisterEntity;
using RineaR.MadeHighlow.Actions.RegisterPlayer;
using RineaR.MadeHighlow.Actions.RegisterTile;
using RineaR.MadeHighlow.Actions.RemoveComponent;
using RineaR.MadeHighlow.Actions.ReserveCommand;
using RineaR.MadeHighlow.Actions.RunCommand;
using RineaR.MadeHighlow.Actions.StartCommands;
using RineaR.MadeHighlow.Actions.SupplyCard;
using RineaR.MadeHighlow.Actions.UnregisterCard;
using RineaR.MadeHighlow.Actions.UnregisterEntity;
using RineaR.MadeHighlow.Actions.UnregisterTile;

namespace RineaR.MadeHighlow.Actions
{
    public interface IActionRunner
    {
        [NotNull]
        ReactedResult<ValidResult> Run([NotNull] IHistory history, [NotNull] IValidAction action);

        [NotNull]
        CreateCardResult CreateCard([NotNull] IHistory history, [NotNull] CreateCardAction action);

        [NotNull]
        DeleteCardResult DeleteCard([NotNull] IHistory history, [NotNull] DeleteCardAction action);

        [NotNull]
        PlaceCardResult PlaceCard([NotNull] IHistory history, [NotNull] PlaceCardAction action);

        [NotNull]
        RegisterCardResult RegisterCard([NotNull] IHistory history, [NotNull] RegisterCardAction action);

        [NotNull]
        UnregisterCardResult UnregisterCard([NotNull] IHistory history, [NotNull] UnregisterCardAction action);

        [NotNull]
        CreateComponentResult CreateComponent([NotNull] IHistory history, [NotNull] CreateComponentAction action);

        [NotNull]
        DeleteComponentResult DeleteComponent([NotNull] IHistory history, [NotNull] DeleteComponentAction action);

        [NotNull]
        RegisterComponentResult RegisterComponent([NotNull] IHistory history, [NotNull] RegisterComponentAction action);

        [NotNull]
        CreateEntityResult CreateEntity([NotNull] IHistory history, [NotNull] CreateEntityAction action);

        [NotNull]
        DeleteEntityResult DeleteEntity([NotNull] IHistory history, [NotNull] DeleteEntityAction action);

        [NotNull]
        MoveEntityResult MoveEntity([NotNull] IHistory history, [NotNull] MoveEntityAction action);

        [NotNull]
        PositionEntityResult PositionEntity([NotNull] IHistory history, [NotNull] PositionEntityAction action);

        [NotNull]
        RegisterEntityResult RegisterEntity([NotNull] IHistory history, [NotNull] RegisterEntityAction action);

        [NotNull]
        UnregisterEntityResult UnregisterEntity([NotNull] IHistory history, [NotNull] UnregisterEntityAction action);

        [NotNull]
        AllocateIDResult AllocateID([NotNull] IHistory history);

        [NotNull]
        IncrementTurnResult IncrementTurn([NotNull] IHistory history, [NotNull] IncrementTurnEvaluator action);

        [NotNull]
        CreatePlayerResult CreatePlayer([NotNull] IHistory history, [NotNull] CreatePlayerAction action);

        [NotNull]
        RegisterPlayerResult RegisterPlayer([NotNull] IHistory history, [NotNull] RegisterPlayerAction action);

        [NotNull]
        CreateTileResult CreateTile([NotNull] IHistory history, [NotNull] CreateTileAction action);

        [NotNull]
        DeleteTileResult DeleteTile([NotNull] IHistory history, [NotNull] DeleteTileAction action);

        [NotNull]
        PositionTileResult PositionTile([NotNull] IHistory history, [NotNull] PositionTileAction action);

        [NotNull]
        RegisterTileResult RegisterTile([NotNull] IHistory history, [NotNull] RegisterTileAction action);

        [NotNull]
        UnregisterTileResult UnregisterTile([NotNull] IHistory history, [NotNull] UnregisterTileAction action);

        [NotNull]
        ReactedResult<DropCardResult> DropCard([NotNull] IHistory history, [NotNull] DropCardAction action);

        [NotNull]
        ReactedResult<PayCardResult> PayCard([NotNull] IHistory history, [NotNull] PayCardAction action);

        [NotNull]
        ReactedResult<SupplyCardResult> SupplyCard([NotNull] IHistory history, [NotNull] SupplyCardAction action);

        [NotNull]
        ReactedResult<ReserveCommandResult> ReserveCommand(
            [NotNull] IHistory history,
            [NotNull] ReserveCommandAction action
        );

        [NotNull]
        ReactedResult<RunCommandResult> RunCommand([NotNull] IHistory history, [NotNull] RunCommandAction action);

        [NotNull]
        ReactedResult<StartCommandsResult> StartCommands(
            [NotNull] IHistory history,
            [NotNull] StartCommandsAction action
        );

        [NotNull]
        ReactedResult<AddComponentResult> AddComponent([NotNull] IHistory history, [NotNull] AddComponentAction action);

        [NotNull]
        ReactedResult<RemoveComponentResult> RemoveComponent(
            [NotNull] IHistory history,
            [NotNull] RemoveComponentAction action
        );

        [NotNull]
        ReactedResult<DestroyEntityResult> DestroyEntity(
            [NotNull] IHistory history,
            [NotNull] DestroyEntityAction action
        );

        [NotNull]
        ReactedResult<EntityFlyResult> EntityFly([NotNull] IHistory history, [NotNull] EntityFlyAction action);

        [NotNull]
        ReactedResult<EntityStepResult> EntityStep([NotNull] IHistory history, [NotNull] EntityStepAction action);

        [NotNull]
        ReactedResult<EntityTeleportResult> EntityTeleport(
            [NotNull] IHistory history,
            [NotNull] EntityTeleportAction action
        );

        [NotNull]
        ReactedResult<EntityWalkResult> EntityWalk([NotNull] IHistory history, [NotNull] EntityWalkAction action);

        [NotNull]
        ReactedResult<GenerateEntityResult> GenerateEntity(
            [NotNull] IHistory history,
            [NotNull] GenerateEntityAction action
        );

        [NotNull]
        ReactedResult<InstantDamageResult> InstantDamage(
            [NotNull] IHistory history,
            [NotNull] InstantDamageAction action
        );

        [NotNull]
        ReactedResult<InstantDeathResult> InstantDeath([NotNull] IHistory history, [NotNull] InstantDeathAction action);

        [NotNull]
        ReactedResult<InstantHealResult> InstantHeal([NotNull] IHistory history, [NotNull] InstantHealAction action);

        [NotNull]
        ReactedResult<InteractResult> Interact([NotNull] IHistory history, [NotNull] InteractAction action);

        [NotNull]
        ReactedResult<KnockBackResult> KnockBack([NotNull] IHistory history, [NotNull] KnockBackAction action);

        [NotNull]
        ReactedResult<BigBangResult> BigBang([NotNull] IHistory history, [NotNull] BigBangAction action);

        [NotNull]
        ReactedResult<UpdateTurnResult> UpdateTurn([NotNull] IHistory history, [NotNull] UpdateTurnAction action);

        [NotNull]
        ReactedResult<JoinPlayerResult> JoinPlayer([NotNull] IHistory history, [NotNull] JoinPlayerAction action);

        [NotNull]
        ReactedResult<DestroyTileResult> DestroyTile([NotNull] IHistory history, [NotNull] DestroyTileAction action);

        [NotNull]
        ReactedResult<ElevateTileResult> ElevateTile([NotNull] IHistory history, [NotNull] ElevateTileAction action);

        [NotNull]
        ReactedResult<GenerateTileResult> GenerateTile([NotNull] IHistory history, [NotNull] GenerateTileAction action);
    }
}
