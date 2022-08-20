using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using RineaR.MadeHighlow.GameData.CardEffects;
using RineaR.MadeHighlow.GameModel;
using RineaR.MadeHighlow.GameModel.Geometry;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RineaR.MadeHighlow.Clients.Local.Strategy.Tools
{
    /// <summary>
    ///     歩行ルートを選択するインタフェース
    /// </summary>
    [RequireComponent(typeof(Explorer))]
    [RequireComponent(typeof(Window))]
    public class WalkRouteSelector : MonoBehaviour, MainInputActions.IWalkRouteSelectorActions
    {
        public Window window;
        public WalkRouteHighlight highlight;
        public Explorer explorer;
        private readonly Subject<WalkRoute> _onSubmit = new();
        private WalkRouteEntry _entry;
        private MainInputActions _input;

        private void Awake()
        {
            _input = new MainInputActions();
            _input.WalkRouteSelector.SetCallbacks(this);
        }

        private void Reset()
        {
            RefreshReferences();
        }

        private void Start()
        {
            RefreshReferences();
        }

        private void OnEnable()
        {
            _input.WalkRouteSelector.Enable();
        }

        private void OnDisable()
        {
            _input.WalkRouteSelector.Disable();
        }

        private void OnDestroy()
        {
            _input.Dispose();
            _onSubmit.Dispose();
            _entry?.Dispose();
        }

        public void OnAddCheckpoint(InputAction.CallbackContext context)
        {
            if (context.performed == false)
            {
                return;
            }

            var target = explorer.Focus.Current;
            if (target == null)
            {
                return;
            }

            AddCheckpoint(target.fieldTransform.position.To2D());
        }

        public void OnUndoCheckpoint(InputAction.CallbackContext context)
        {
            if (context.performed == false)
            {
                return;
            }

            UndoCheckpoint();
        }

        public void OnResetCheckpoints(InputAction.CallbackContext context)
        {
            if (context.performed == false)
            {
                return;
            }

            ResetCheckpoints();
        }

        public void OnSubmit(InputAction.CallbackContext context)
        {
            if (context.performed == false)
            {
                return;
            }

            var target = explorer.Focus.Current;
            if (target == null)
            {
                return;
            }

            // ダブルクリックしたタイルが最後に選択したタイルだった時のみ、ルート確定とする
            if (target.fieldTransform.position.To2D() == _entry.LatestCheckpoint)
            {
                SubmitEntry();
            }
        }

        private void RefreshReferences()
        {
            explorer = GetComponent<Explorer>() ?? throw new NullReferenceException();
            window = GetComponent<Window>() ?? throw new NullReferenceException();
            highlight = GetComponentInChildren<WalkRouteHighlight>();
        }

        public UniTask<WalkRoute> SelectWalkRoute(Figure walker, CancellationToken token)
        {
            StartEntry(walker);
            return _onSubmit.First().ToUniTask(cancellationToken: token);
        }

        public void StartEntry(Figure walker)
        {
            _entry = new WalkRouteEntry(walker);
            if (highlight != null)
            {
                highlight.SetEntry(_entry);
            }
        }

        public void AddCheckpoint(FieldVector2 position)
        {
            if (_entry == null)
            {
                return;
            }

            _entry.AddCheckpoint(position);
        }

        public void UndoCheckpoint()
        {
            if (_entry == null)
            {
                return;
            }

            _entry.UndoCheckpoint();
        }

        public void ResetCheckpoints()
        {
            if (_entry == null)
            {
                return;
            }

            _entry.ResetCheckpoints();
        }

        public void SubmitEntry()
        {
            if (_entry == null)
            {
                return;
            }

            var prediction = _entry.CurrentPrediction;
            _onSubmit.OnNext(prediction.Route);

            _entry.Dispose();
            _entry = null;
        }
    }
}