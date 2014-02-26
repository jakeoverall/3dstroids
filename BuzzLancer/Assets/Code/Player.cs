using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    public class Player : MonoBehaviour
    {
        public Camera Camera;
        public Destroyable Destroyable;
        public BasicWeapon BasicWeapon;

        private PlayerCamera _camera;
        private PlayerController _controller;
        private PlayerGUI _playerGUI;
        private PlayerWeapons _weapons;

        private IEnumerable<BasicWeaponMount> _mounts;

        public float Health { get { return Destroyable.Health; } }
        public float MaxHealth { get { return Destroyable.MaxHealth; } }

        public void Awake()
        {
            _mounts = GetComponentsInChildren<BasicWeaponMount>();

            _camera = new PlayerCamera(this, Camera);
            _controller = new PlayerController(this);
            _playerGUI = new PlayerGUI(this, _controller);
            _weapons = new PlayerWeapons(this, Camera, _controller, _mounts);

            Equip(BasicWeapon);
        }

        public void Equip(BasicWeapon weapon)
        {
            foreach (var mount in _mounts)
                mount.Equip(weapon);
        }

        public void Update()
        {
            _controller.Update();
            _playerGUI.Update();
            _camera.Update();
            _weapons.Update();
        }

        public void OnGUI()
        {
            _playerGUI.OnGUI();
        }
    }   
}

