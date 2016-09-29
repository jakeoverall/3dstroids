using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    public class PlayerController
    {
        private readonly Ship _ship;
        private float
            _baseVelocity,
            _targetVelocity,
            _variableVelocity;

        private bool _moving;

        public Vector3 MousePosition { get; private set; }

        public float CurrentVelocity { get; private set; }

        public float MaxVariableVelocity { get; set; }
        public float MinimumVelocity { get; set; }

        public float Acceleration { get; set; }

        public float VelocityDamp { get; set; }

        public float RotationSpeed { get; set; }

        public bool UseRelativeMovement { get; set; }

        public Vector2 MouseSensitivity { get; set; }

        public float AfterBurnerModifier { get; set; }

        public float StrafeModifier { get; set; }

        public PlayerController(Ship ship)
        {
            MaxVariableVelocity = 20;
            Acceleration = 70;
            VelocityDamp = 20;
            RotationSpeed = 0.3f;
            AfterBurnerModifier = 50;
            StrafeModifier = 30;
            MouseSensitivity = new Vector2(700, 700);
            UseRelativeMovement = false;
            _moving = false;
            _ship = ship;
        }

        public void Update()
        {
            Screen.lockCursor = UseRelativeMovement;

            if (UseRelativeMovement)
            {
                MousePosition += new Vector3(
                    Input.GetAxis("Mouse X") * Time.deltaTime * MouseSensitivity.x,
                    Input.GetAxis("Mouse Y") * Time.deltaTime * MouseSensitivity.y);
            }
            else
                MousePosition = Input.mousePosition;

            UpdatePosition();
            UpdateRotation();
        }

        private void toggleStreamsPower(float power)
        {
            foreach (var stream in _ship.EngineMount.Jetstreams)
            {
                stream.startSpeed = power;
            }
        }

        private void toggleStreams(bool on)
        {
            if (on == true && _moving == false)
            {
                _moving = on;
                foreach (var stream in _ship.EngineMount.Jetstreams)
                {
                    stream.Play();
                }
            }
            else if (on == false && _moving == true)
            {
                _moving = false;
                foreach (var stream in _ship.EngineMount.Jetstreams)
                {
                    stream.Stop();
                }
            }
        }

        private void UpdatePosition()
        {
            _variableVelocity = Mathf.Clamp(_variableVelocity + Input.GetAxis("Vertical") * Time.deltaTime * Acceleration,
                0,
                MaxVariableVelocity);

            _targetVelocity = _variableVelocity + MinimumVelocity;

            if (Input.GetKey(KeyCode.B))
            {
                _targetVelocity += AfterBurnerModifier;
            }

            CurrentVelocity = Mathf.Lerp(CurrentVelocity, _targetVelocity, Time.deltaTime * VelocityDamp);
            toggleStreamsPower(_targetVelocity);

            if (_targetVelocity > 0)
            {
                toggleStreams(true);
            }
            else {
                toggleStreams(false);
            }

            _ship.transform.Translate(
                Input.GetAxis("Horizontal") * Time.deltaTime * StrafeModifier,
                0,
                CurrentVelocity * Time.deltaTime,
                Space.Self);
        }

        private void UpdateRotation()
        {
            if (Input.GetKey("e"))
            {
                _ship.transform.Rotate(0, 0, -90f * Time.deltaTime);
            }

            if (Input.GetKey("q"))
            {
                _ship.transform.Rotate(0, 0, 90f * Time.deltaTime);
            }

            var mouseMovement = (MousePosition - (new Vector3(Screen.width / 2f, Screen.height / 2f))) * .01f;

            if (mouseMovement.sqrMagnitude >= .5)
            {
                _ship.transform.Rotate(new Vector3(-mouseMovement.y, mouseMovement.x, 0) * RotationSpeed);
            }
        }
    }
}
