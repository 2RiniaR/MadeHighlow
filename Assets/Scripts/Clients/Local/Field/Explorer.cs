using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RineaR.MadeHighlow.Clients.Local.Field
{
    public class Explorer : MonoBehaviour, MainInputActions.IWorldExplorerActions
    {
        public float zoomCameraSpeed = 0.2f;
        public float minCameraDistance = 5f;
        public float maxCameraDistance = 40f;
        public float moveCameraSpeed = 1f;
        public LayerMask focusLayerMask;
        public float maxFocusDistance = Mathf.Infinity;
        public ExplorerFocusViewer focusViewer;
        public ExplorerFocusHighlight focusHighlight;
        private CameraController _cameraController;
        private MainInputActions _input;

        public Focus Focus { get; private set; }

        private void Awake()
        {
            _input = new MainInputActions();
            _input.WorldExplorer.SetCallbacks(this);
        }

        private void Reset()
        {
            RefreshReferences();
        }

        private void Start()
        {
            RefreshReferences();
            Focus = new Focus();
            var camera = GetComponentInChildren<CinemachineVirtualCamera>();
            var transposer = camera.GetCinemachineComponent<CinemachineFramingTransposer>();
            _cameraController = new CameraController(camera, transposer);
        }

        private void Update()
        {
            UpdateCamera();
            UpdateFocus();
            UpdateView();
        }

        private void OnEnable()
        {
            _input.WorldExplorer.Enable();
        }

        private void OnDisable()
        {
            _input.WorldExplorer.Disable();
        }

        private void OnDestroy()
        {
            _input.Dispose();
        }

        public void OnFocus(InputAction.CallbackContext context)
        {
            /* Update() で処理を行うため、何もしない */
        }

        public void OnMoveCamera(InputAction.CallbackContext context)
        {
            /* Update() で処理を行うため、何もしない */
        }

        public void OnZoomCamera(InputAction.CallbackContext context)
        {
            if (context.performed == false) return;
            ZoomCamera(context.ReadValue<float>());
        }

        private void RefreshReferences()
        {
            focusViewer = GetComponentInChildren<ExplorerFocusViewer>();
            focusHighlight = GetComponentInChildren<ExplorerFocusHighlight>();
        }

        private void UpdateCamera()
        {
            _cameraController.ZoomSpeed = zoomCameraSpeed;
            _cameraController.MinDistance = minCameraDistance;
            _cameraController.MaxDistance = maxCameraDistance;
            _cameraController.MoveSpeed = moveCameraSpeed;
            MoveCamera(_input.WorldExplorer.MoveCamera.ReadValue<Vector2>());
        }

        private void UpdateFocus()
        {
            Focus.TargetLayerMask = focusLayerMask;
            Focus.MaxDistance = maxFocusDistance;
            FocusAt(_input.WorldExplorer.Focus.ReadValue<Vector2>());
        }

        private void UpdateView()
        {
            focusViewer.source = Focus.Current;
            focusHighlight.source = Focus.Current;
        }

        public void MoveCamera(Vector2 value)
        {
            _cameraController.MoveCamera(value);
        }

        public void ZoomCamera(float value)
        {
            _cameraController.ZoomCamera(value);
        }

        public void FocusAt(Vector2 screenPosition)
        {
            Focus.SetTileAt(screenPosition);
        }
    }
}