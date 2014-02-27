using UnityEngine;

namespace Assets.Code
{
    public class PlayerCamera
    {
        private readonly Player _player;
        private readonly Camera _camera;

        private const float Height = 1.1f;
        private const float Distance = 6.0f;
        private const float Damping = 5.0f;

        public PlayerCamera(Player player, Camera camera)
        {
            _player = player;
            _camera = camera;
        }

        public void Update()
        {
            var position = _player.transform.TransformPoint(0, Height, -Distance);
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, position, Time.deltaTime * Damping);

            _camera.transform.LookAt(_player.transform.TransformPoint(0, 0, 50), _player.transform.up);
        }

    }
}
