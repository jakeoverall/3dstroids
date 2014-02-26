using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code
{
    public class PlayerWeapons
    {
        private readonly Player _player;
        private readonly Camera _camera;
        private readonly PlayerController _controller;
        private readonly IEnumerable<BasicWeaponMount> _basicWeapons; 

        public PlayerWeapons(Player player, Camera camera, PlayerController controller, IEnumerable<BasicWeaponMount> basicWeapons)
        {
            _player = player;
            _camera = camera;
            _controller = controller;
            _basicWeapons = basicWeapons;
        }

        public void Update()
        {
            if (!Input.GetMouseButton(0))
                return;

            var ray = _camera.ScreenPointToRay(_controller.MousePosition);
            var direction = (ray.origin + ray.direction * 100) - _player.transform.position;
            direction.Normalize();

            foreach (var weapon in _basicWeapons)
            {
                weapon.Fire(direction);
            }
        }
    }
}
