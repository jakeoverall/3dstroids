using UnityEngine;

namespace Assets.Code
{
    public class Player : MonoBehaviour
    {
        public Camera Camera;

        private PlayerCamera _camera;
        private PlayerController _controller;

        public void Awake()
        {
            _camera = new PlayerCamera(this, Camera);
            _controller = new PlayerController(this);
        }

        public void Update()
        {
            _controller.Update();
            _camera.Update();
        }
    }   
}

