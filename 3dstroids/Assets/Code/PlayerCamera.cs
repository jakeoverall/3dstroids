using UnityEngine;

namespace Assets.Code
{
    public class PlayerCamera
    {
        private readonly Ship _ship;
        private readonly Camera _camera;

        private const float Height = 1.1f;
        private const float Distance = 6.0f;
        private const float Damping = 5.0f;

        public PlayerCamera(Ship ship, Camera camera)
        {
            _ship = ship;
            _camera = camera;
        }

        public void Update()
        {
            var position = _ship.transform.TransformPoint(0, Height, -Distance);
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, position, Time.deltaTime * Damping);
            _camera.transform.LookAt(_ship.transform.TransformPoint(0, 0, 50), _ship.transform.up);
        }

    }
}
