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
    public class StrategySelector : MonoBehaviour, IStrategySelector, MainInputActions.IStrategySelectorActions
    {
        [Header("Requirements")]
        public Window window;

        public Client client;

        [Header("Settings")]
        public StrategyChecker strategyChecker;

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

        private void Start()
        {
            window = GetComponent<Window>() ?? throw new NullReferenceException();
            client = GetComponentInParent<Client>();
            if (client != null) client.StrategySelector = this;
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
            _onSubmit.Dispose();
        }

        public async UniTask SelectStrategy(Player submitter, CancellationToken token)
        {
            selfPlayerView.SetSource(submitter);
            await _onSubmit.First().ToUniTask(cancellationToken: token);
            window.Close();
        }

        public void OnSubmit(InputAction.CallbackContext context)
        {
            if (context.performed == false) return;
            Submit();
        }

        public void Submit()
        {
            if (!strategyChecker.IsValid()) return;
            _onSubmit.OnNext(Unit.Default);
        }
    }
}