using System;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.AddComponent;
using RineaR.MadeHighlow.Actions.AllocateID;
using RineaR.MadeHighlow.Actions.BigBang;
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
using RineaR.MadeHighlow.Actions.UpdateTurn;

namespace RineaR.MadeHighlow.Actions
{
    public class ActionRunner : IActionRunner
    {
        public ActionRunner([NotNull] EvaluationContext context)
        {
            Context = context;
            Reaction = new ReactionEvaluator(Context);
        }

        [NotNull] private EvaluationContext Context { get; }
        [NotNull] private ReactionEvaluator Reaction { get; }

        public ReactedResult<IValidResult> Run(IHistory history, IValidAction action)
        {
            return Reaction.Evaluate(history, action, initial => action.Evaluate(this, initial));
        }

        public CreateCardResult CreateCard(IHistory history, CreateCardAction action)
        {
            return new CreateCardEvaluator(Context, history, action).Evaluate();
        }

        public DeleteCardResult DeleteCard(IHistory history, DeleteCardAction action)
        {
            return new DeleteCardEvaluator(Context, history, action).Evaluate();
        }

        public PlaceCardResult PlaceCard(IHistory history, PlaceCardAction action)
        {
            return new PlaceCardEvaluator(Context, history, action).Evaluate();
        }

        public RegisterCardResult RegisterCard(IHistory history, RegisterCardAction action)
        {
            return new RegisterCardEvaluator(Context, history, action).Evaluate();
        }

        public UnregisterCardResult UnregisterCard(IHistory history, UnregisterCardAction action)
        {
            return new UnregisterCardEvaluator(Context, history, action).Evaluate();
        }

        public CreateComponentResult CreateComponent(IHistory history, CreateComponentAction action)
        {
            return new CreateComponentEvaluator(Context, history, action).Evaluate();
        }

        public DeleteComponentResult DeleteComponent(IHistory history, DeleteComponentAction action)
        {
            return new DeleteComponentEvaluator(Context, history, action).Evaluate();
        }

        public RegisterComponentResult RegisterComponent(IHistory history, RegisterComponentAction action)
        {
            return new RegisterComponentEvaluator(Context, history, action).Evaluate();
        }

        public CreateEntityResult CreateEntity(IHistory history, CreateEntityAction action)
        {
            return new CreateEntityEvaluator(Context, history, action).Evaluate();
        }

        public DeleteEntityResult DeleteEntity(IHistory history, DeleteEntityAction action)
        {
            return new DeleteEntityEvaluator(Context, history, action).Evaluate();
        }

        public MoveEntityResult MoveEntity(IHistory history, MoveEntityAction action)
        {
            return new MoveEntityEvaluator(Context, history, action).Evaluate();
        }

        public PositionEntityResult PositionEntity(IHistory history, PositionEntityAction action)
        {
            return new PositionEntityEvaluator(Context, history, action).Evaluate();
        }

        public RegisterEntityResult RegisterEntity(IHistory history, RegisterEntityAction action)
        {
            return new RegisterEntityEvaluator(Context, history, action).Evaluate();
        }

        public UnregisterEntityResult UnregisterEntity(IHistory history, UnregisterEntityAction action)
        {
            return new UnregisterEntityEvaluator(Context, history, action).Evaluate();
        }

        public AllocateIDResult AllocateID(IHistory history)
        {
            return new AllocateIDEvaluator(history).Evaluate();
        }

        public IncrementTurnResult IncrementTurn(IHistory history)
        {
            return new IncrementTurnEvaluator(history).Evaluate();
        }

        public CreatePlayerResult CreatePlayer(IHistory history, CreatePlayerAction action)
        {
            return new CreatePlayerEvaluator(Context, history, action).Evaluate();
        }

        public RegisterPlayerResult RegisterPlayer(IHistory history, RegisterPlayerAction action)
        {
            return new RegisterPlayerEvaluator(Context, history, action).Evaluate();
        }

        public CreateTileResult CreateTile(IHistory history, CreateTileAction action)
        {
            return new CreateTileEvaluator(Context, history, action).Evaluate();
        }

        public DeleteTileResult DeleteTile(IHistory history, DeleteTileAction action)
        {
            return new DeleteTileEvaluator(Context, history, action).Evaluate();
        }

        public PositionTileResult PositionTile(IHistory history, PositionTileAction action)
        {
            return new PositionTileEvaluator(Context, history, action).Evaluate();
        }

        public RegisterTileResult RegisterTile(IHistory history, RegisterTileAction action)
        {
            return new RegisterTileEvaluator(Context, history, action).Evaluate();
        }

        public UnregisterTileResult UnregisterTile(IHistory history, UnregisterTileAction action)
        {
            return new UnregisterTileEvaluator(Context, history, action).Evaluate();
        }

        public ReactedResult<DropCardResult> DropCard(IHistory history, DropCardAction action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new DropCardEvaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<PayCardResult> PayCard(IHistory history, PayCardAction action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new PayCardEvaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<SupplyCardResult> SupplyCard(IHistory history, SupplyCardAction action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new SupplyCardEvaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<ReserveCommandResult> ReserveCommand(IHistory history, ReserveCommandAction action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new ReserveCommandEvaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<RunCommandResult> RunCommand(IHistory history, RunCommandAction action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new RunCommandEvaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<StartCommandsResult> StartCommands(IHistory history, StartCommandsAction action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new StartCommandsEvaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<AddComponentResult> AddComponent(IHistory history, AddComponentAction action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new AddComponentEvaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<RemoveComponentResult> RemoveComponent(IHistory history, RemoveComponentAction action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new RemoveComponentEvaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<DestroyEntityResult> DestroyEntity(IHistory history, DestroyEntityAction action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new DestroyEntityEvaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<EntityFlyResult> EntityFly(IHistory history, EntityFlyAction action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new EntityFlyEvaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<EntityStepResult> EntityStep(IHistory history, EntityStepAction action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new EntityStepEvaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<EntityTeleportResult> EntityTeleport(IHistory history, EntityTeleportAction action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new EntityTeleportEvaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<EntityWalkResult> EntityWalk(IHistory history, EntityWalkAction action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new EntityWalkEvaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<GenerateEntityResult> GenerateEntity(IHistory history, GenerateEntityAction action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new GenerateEntityEvaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<InstantDamageResult> InstantDamage(IHistory history, InstantDamageAction action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new InstantDamageEvaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<InstantDeathResult> InstantDeath(IHistory history, InstantDeathAction action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new InstantDeathEvaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<InstantHealResult> InstantHeal(IHistory history, InstantHealAction action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new InstantHealEvaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<InteractResult> Interact(IHistory history, InteractAction action)
        {
            throw new NotImplementedException();
        }

        public ReactedResult<KnockBackResult> KnockBack(IHistory history, KnockBackAction action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new KnockBackEvaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<BigBangResult> BigBang(IHistory history, BigBangAction action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new BigBangEvaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<UpdateTurnResult> UpdateTurn(IHistory history, UpdateTurnAction action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new UpdateTurnEvaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<JoinPlayerResult> JoinPlayer(IHistory history, JoinPlayerAction action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new JoinPlayerEvaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<DestroyTileResult> DestroyTile(IHistory history, DestroyTileAction action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new DestroyTileEvaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<ElevateTileResult> ElevateTile(IHistory history, ElevateTileAction action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new ElevateTileEvaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<GenerateTileResult> GenerateTile(IHistory history, GenerateTileAction action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new GenerateTileEvaluator(Context, initial, action).Evaluate()
            );
        }
    }
}
