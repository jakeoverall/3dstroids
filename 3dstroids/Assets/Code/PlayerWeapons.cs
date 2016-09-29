using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    public class PlayerWeapons
    {
        private readonly Ship _ship;
        private readonly Camera _camera;
        private readonly PlayerController _controller;
        private readonly IEnumerable<BasicWeaponMount> _basicWeapons;

        public PlayerWeapons(Ship ship, Camera camera, PlayerController controller, IEnumerable<BasicWeaponMount> basicWeapons)
        {
            _ship = ship;
            _camera = camera;
            _controller = controller;
            _basicWeapons = basicWeapons;
        }

        public void Update()
        {
            if (!Input.GetMouseButton(0))
                return;
            
            var ray = _camera.ScreenPointToRay(_controller.MousePosition);
            var direction = (ray.origin + ray.direction * 100) - _ship.transform.position;
            direction.Normalize();

            foreach (var weapon in _basicWeapons)
            {
                weapon.Fire(direction);
            }
        }
    }
}
