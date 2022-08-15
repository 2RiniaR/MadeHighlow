using Cinemachine;
using UnityEngine;

namespace RineaR.MadeHighlow.Clients.Local.Field
{
    public class CameraController
    {
        private readonly CinemachineVirtualCamera _camera;
        private readonly CinemachineFramingTransposer _transposer;

        public CameraController(CinemachineVirtualCamera camera, CinemachineFramingTransposer transposer)
        {
            _camera = camera;
            _transposer = transposer;
        }

        public float ZoomSpeed { get; set; } = 0.2f;
        public float MinDistance { get; set; } = 5f;
        public float MaxDistance { get; set; } = 40f;
        public float MoveSpeed { get; set; } = 1f;

        public void MoveCamera(Vector2 value)
        {
            var delta = value * MoveSpeed * Time.deltaTime;
            _camera.Follow.position += new Vector3(delta.x, 0, delta.y);
        }

        public void ZoomCamera(float value)
        {
            var delta = value * ZoomSpeed;
            _transposer.m_CameraDistance = Mathf.Clamp(_transposer.m_CameraDistance - delta,
                MinDistance, MaxDistance);
        }
    }
}