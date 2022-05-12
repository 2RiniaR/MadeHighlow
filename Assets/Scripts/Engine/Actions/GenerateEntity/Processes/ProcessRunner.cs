using System;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.AddComponent;
using RineaR.MadeHighlow.Actions.GenerateEntity.PositionEntity;
using RineaR.MadeHighlow.Actions.GenerateEntity.RegisterEntity;

namespace RineaR.MadeHighlow.Actions.GenerateEntity
{
    public class ProcessRunner
    {
        public ProcessRunner([NotNull] GenerateEntityAction action, ref IActionContext context)
        {
            Action = action;
            Context = context;
        }

        [NotNull] private GenerateEntityAction Action { get; }
        [NotNull] private IActionContext Context { get; set; }
        [NotNull] private RunningProcess Process { get; set; } = new();

        [CanBeNull] public FailedProcess Failed { get; private set; }
        [CanBeNull] public SucceedProcess Succeed { get; private set; }

        public bool TryProcess()
        {
            if (!TryRegisterEntity(out var registerEntity))
            {
                Failed = Process.AsFailed with { RegisterEntity = registerEntity };
                return false;
            }

            var entityID = (Process.RegisterEntity ?? throw new NullReferenceException()).RegisteredEntity.EntityID;

            if (!TryAddComponents(entityID, out var addComponents))
            {
                Failed = Process.AsFailed with { AddComponents = addComponents };
                return false;
            }

            if (!TryPositionEntity(entityID, out var positionEntity))
            {
                Failed = Process.AsFailed with { PositionEntity = positionEntity };
                return false;
            }

            Succeed = Process.AsSucceed;
            return true;
        }

        private bool TryRegisterEntity([NotNull] out RegisterEntityResult result)
        {
            result = new RegisterEntityAction(Action.Status).Evaluate(Context);
            Context = Context.Appended(result);

            if (result is not RegisterEntity.SucceedResult succeedResult)
            {
                return false;
            }

            Process = Process with { RegisterEntity = succeedResult };
            return true;
        }

        private bool TryAddComponents([NotNull] EntityID entityID, [NotNull] out ValueList<AddComponentResult> results)
        {
            results = ValueList<AddComponentResult>.Empty;

            foreach (var component in Action.Status.Components)
            {
                var attempt = TryAddComponentItem(entityID, component, out var result);
                results = results.Add(result);
                if (!attempt)
                {
                    return false;
                }
            }

            return true;
        }

        private bool TryAddComponentItem(
            [NotNull] EntityID entityID,
            [NotNull] Component component,
            [NotNull] out AddComponentResult result
        )
        {
            result = new AddComponentAction(entityID, component).Evaluate(Context);
            Context = Context.Appended(result);

            if (result is not AddComponent.SucceedResult succeedResult)
            {
                return false;
            }

            Process = Process with
            {
                AddComponents
                = (Process.AddComponents ?? ValueList<AddComponent.SucceedResult>.Empty).Add(succeedResult),
            };
            return true;
        }

        private bool TryPositionEntity([NotNull] EntityID entityID, [NotNull] out PositionEntityResult result)
        {
            result = new PositionEntityAction(entityID, Action.Status.Position3D).Evaluate(Context);
            Context = Context.Appended(result);

            if (result is not PositionEntity.SucceedResult succeedResult)
            {
                return false;
            }

            Process = Process with { PositionEntity = succeedResult };
            return true;
        }
    }
}
