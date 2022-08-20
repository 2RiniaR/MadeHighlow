using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using RineaR.MadeHighlow.GameModel;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RineaR.MadeHighlow.Clients.Local.Strategy
{
    /// <summary>
    ///     行動選択ウィンドウ
    /// </summary>
    [RequireComponent(typeof(Window))]
    public class StrategySelector : MonoBehaviour, MainInputActions.IStrategySelectorActions
    {
        [Header("Requirements")]
        public Window window;

        [Header("Settings")]
        public StrategyChecker strategyChecker;

        [Header("References on scene")]
        public Player player;

        [Header("Views")]
        public SelfPlayerView selfPlayerView;

        private readonly Subject<Unit> _onSubmit = new();
        private CancellationTokenSource _cancellationTokenSource;
        private MainInputActions _input;

        private void Awake()
        {
            _input = new MainInputActions();
            _input.StrategySelector.SetCallbacks(this);
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
            _cancellationTokenSource = new CancellationTokenSource();
            _input.StrategySelector.Enable();
        }

        private void OnDisable()
        {
            _input.StrategySelector.Disable();
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }

        private void OnDestroy()
        {
            _input.Dispose();
            _onSubmit.Dispose();
        }

        public void OnSubmit(InputAction.CallbackContext context)
        {
            if (context.performed == false)
            {
                return;
            }

            Submit();
        }

        private void RefreshReferences()
        {
            window = GetComponent<Window>() ?? throw new NullReferenceException();
        }

        /// <summary>
        ///     戦略を選択させ、完了するまで待つ。
        /// </summary>
        public async UniTask SelectStrategy(CancellationToken token)
        {
            selfPlayerView.SetSource(player);
            await _onSubmit.First().ToUniTask(cancellationToken: token);
            await UniTask.Yield(token);
        }

        public void Submit()
        {
            if (!strategyChecker.IsValid())
            {
                return;
            }

            _onSubmit.OnNext(Unit.Default);
        }
    }
}