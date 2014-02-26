using UnityEngine;

namespace Assets.Code
{
    public class Player : MonoBehaviour
    {
        public Camera Camera;
        public Destroyable Destroyable;

        private PlayerCamera _camera;
        private PlayerController _controller;
        private PlayerGUI _playerGUI;

        public float Health { get { return Destroyable.Health; } }
        public float MaxHealth { get { return Destroyable.MaxHealth; } }

        public void Awake()
        {
            _camera = new PlayerCamera(this, Camera);
            _controller = new PlayerController(this);
            _playerGUI = new PlayerGUI(this, _controller);
        }

        public void Update()
        {
            _controller.Update();
            _playerGUI.Update();
            _camera.Update();
        }

        public void OnGUI()
        {
            _playerGUI.OnGUI();
        }
    }   
}

