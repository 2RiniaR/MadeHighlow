using System;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.CreateCard;
using Action = RineaR.MadeHighlow.Actions.CreateCard.Action;

namespace RineaR.MadeHighlow.Actions
{
    public class ActionRunner : IActionRunner
    {
        public ActionRunner([NotNull] IEvaluationContext context)
        {
            Context = context;
            Reaction = new ReactionEvaluator(Context);
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private ReactionEvaluator Reaction { get; }

        public ReactedResult<IValidResult> Run(IHistory history, IValidAction action)
        {
            return Reaction.Evaluate(history, action, initial => action.Evaluate(this, initial));
        }

        public Result CreateCard(IHistory history, Action action)
        {
            return new Evaluator(Context, history, action).Evaluate();
        }

        public DeleteCard.Result DeleteCard(IHistory history, DeleteCard.Action action)
        {
            return new DeleteCard.Evaluator(Context, history, action).Evaluate();
        }

        public PlaceCard.Result PlaceCard(IHistory history, PlaceCard.Action action)
        {
            return new PlaceCard.Evaluator(Context, history, action).Evaluate();
        }

        public RegisterCard.Result RegisterCard(IHistory history, RegisterCard.Action action)
        {
            return new RegisterCard.Evaluator(Context, history, action).Evaluate();
        }

        public CreateComponent.Result CreateComponent(IHistory history, CreateComponent.Action action)
        {
            return new CreateComponent.Evaluator(Context, history, action).Evaluate();
        }

        public DeleteComponent.Result DeleteComponent(IHistory history, DeleteComponent.Action action)
        {
            return new DeleteComponent.Evaluator(Context, history, action).Evaluate();
        }

        public RegisterComponent.Result RegisterComponent(IHistory history, RegisterComponent.Action action)
        {
            return new RegisterComponent.Evaluator(Context, history, action).Evaluate();
        }

        public CreateEntity.Result CreateEntity(IHistory history, CreateEntity.Action action)
        {
            return new CreateEntity.Evaluator(Context, history, action).Evaluate();
        }

        public DeleteEntity.Result DeleteEntity(IHistory history, DeleteEntity.Action action)
        {
            return new DeleteEntity.Evaluator(Context, history, action).Evaluate();
        }

        public MoveEntity.Result MoveEntity(IHistory history, MoveEntity.Action action)
        {
            return new MoveEntity.Evaluator(Context, history, action).Evaluate();
        }

        public PositionEntity.Result PositionEntity(IHistory history, PositionEntity.Action action)
        {
            return new PositionEntity.Evaluator(Context, history, action).Evaluate();
        }

        public RegisterEntity.Result RegisterEntity(IHistory history, RegisterEntity.Action action)
        {
            return new RegisterEntity.Evaluator(Context, history, action).Evaluate();
        }

        public AllocateID.Result AllocateID(IHistory history)
        {
            return new AllocateID.Evaluator(history).Evaluate();
        }

        public IncrementTurn.Result IncrementTurn(IHistory history)
        {
            return new IncrementTurn.Evaluator(history).Evaluate();
        }

        public CreatePlayer.Result CreatePlayer(IHistory history, CreatePlayer.Action action)
        {
            return new CreatePlayer.Evaluator(Context, history, action).Evaluate();
        }

        public RegisterPlayer.Result RegisterPlayer(IHistory history, RegisterPlayer.Action action)
        {
            return new RegisterPlayer.Evaluator(Context, history, action).Evaluate();
        }

        public CreateTile.Result CreateTile(IHistory history, CreateTile.Action action)
        {
            return new CreateTile.Evaluator(Context, history, action).Evaluate();
        }

        public DeleteTile.Result DeleteTile(IHistory history, DeleteTile.Action action)
        {
            return new DeleteTile.Evaluator(Context, history, action).Evaluate();
        }

        public PositionTile.Result PositionTile(IHistory history, PositionTile.Action action)
        {
            return new PositionTile.Evaluator(Context, history, action).Evaluate();
        }

        public RegisterTile.Result RegisterTile(IHistory history, RegisterTile.Action action)
        {
            return new RegisterTile.Evaluator(Context, history, action).Evaluate();
        }

        public ReactedResult<DropCard.Result> DropCard(IHistory history, DropCard.Action action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new DropCard.Evaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<PayCard.Result> PayCard(IHistory history, PayCard.Action action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new PayCard.Evaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<SupplyCard.Result> SupplyCard(IHistory history, SupplyCard.Action action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new SupplyCard.Evaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<ReserveCommand.Result> ReserveCommand(IHistory history, ReserveCommand.Action action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new ReserveCommand.Evaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<RunCommand.Result> RunCommand(IHistory history, RunCommand.Action action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new RunCommand.Evaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<StartCommands.Result> StartCommands(IHistory history, StartCommands.Action action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new StartCommands.Evaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<AddComponent.Result> AddComponent(IHistory history, AddComponent.Action action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new AddComponent.Evaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<RemoveComponent.Result> RemoveComponent(IHistory history, RemoveComponent.Action action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new RemoveComponent.Evaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<DestroyEntity.Result> DestroyEntity(IHistory history, DestroyEntity.Action action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new DestroyEntity.Evaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<EntityFly.Result> EntityFly(IHistory history, EntityFly.Action action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new EntityFly.Evaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<EntityStep.Result> EntityStep(IHistory history, EntityStep.Action action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new EntityStep.Evaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<EntityTeleport.Result> EntityTeleport(IHistory history, EntityTeleport.Action action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new EntityTeleport.Evaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<EntityWalk.Result> EntityWalk(IHistory history, EntityWalk.Action action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new EntityWalk.Evaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<GenerateEntity.Result> GenerateEntity(IHistory history, GenerateEntity.Action action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new GenerateEntity.Evaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<InstantDamage.Result> InstantDamage(IHistory history, InstantDamage.Action action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new InstantDamage.Evaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<InstantDeath.Result> InstantDeath(IHistory history, InstantDeath.Action action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new InstantDeath.Evaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<InstantHeal.Result> InstantHeal(IHistory history, InstantHeal.Action action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new InstantHeal.Evaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<InteractResult> Interact(IHistory history, InteractAction action)
        {
            throw new NotImplementedException();
        }

        public ReactedResult<KnockBack.Result> KnockBack(IHistory history, KnockBack.Action action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new KnockBack.Evaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<BigBang.Result> BigBang(IHistory history, BigBang.Action action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new BigBang.Evaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<UpdateTurn.Result> UpdateTurn(IHistory history, UpdateTurn.Action action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new UpdateTurn.Evaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<JoinPlayer.Result> JoinPlayer(IHistory history, JoinPlayer.Action action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new JoinPlayer.Evaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<DestroyTile.Result> DestroyTile(IHistory history, DestroyTile.Action action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new DestroyTile.Evaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<ElevateTile.Result> ElevateTile(IHistory history, ElevateTile.Action action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new ElevateTile.Evaluator(Context, initial, action).Evaluate()
            );
        }

        public ReactedResult<GenerateTile.Result> GenerateTile(IHistory history, GenerateTile.Action action)
        {
            return Reaction.Evaluate(
                history,
                action,
                initial => new GenerateTile.Evaluator(Context, initial, action).Evaluate()
            );
        }
    }
}
