using UnityEngine;

namespace Assets.Code
{
    public class PlayerController
    {
        private readonly Player _player;

        private float
            _baseVelocity,
            _targetVelocity,
            _variableVelocity;

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

        public PlayerController(Player player)
        {
            MaxVariableVelocity = 20;
            Acceleration = 70;
            VelocityDamp = 20;
            RotationSpeed = 0.3f;
            AfterBurnerModifier = 50;
            StrafeModifier = 30;

            MouseSensitivity = new Vector2(60, 60);
            UseRelativeMovement = false;

            _player = player;
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

        private void UpdatePosition()
        {
            _variableVelocity = Mathf.Clamp(_variableVelocity + Input.GetAxis("Vertical") * Time.deltaTime * Acceleration,
                0,
                MaxVariableVelocity);

            _targetVelocity = _variableVelocity + MinimumVelocity;

            if (Input.GetKey(KeyCode.Tab))
            {
                _targetVelocity += AfterBurnerModifier;
            }

            CurrentVelocity = Mathf.Lerp(CurrentVelocity, _targetVelocity, Time.deltaTime * VelocityDamp);

            _player.transform.Translate(
                Input.GetAxis("Horizontal") * Time.deltaTime * StrafeModifier,
                0,
                CurrentVelocity * Time.deltaTime,
                Space.Self);
        }
        
        private void UpdateRotation()
        {
            if (Input.GetKey("e"))
            {
                _player.transform.Rotate(0, 0, -90f * Time.deltaTime);
            }

            if (Input.GetKey("q"))
            {
                _player.transform.Rotate(0, 0, 90f * Time.deltaTime);
            }

            var mouseMovement = (MousePosition - (new Vector3(Screen.width / 2f, Screen.height / 2f))) * .2f;

            if (mouseMovement.sqrMagnitude >= 6)
            {
                _player.transform.Rotate(new Vector3(-mouseMovement.y, mouseMovement.x, 0) * RotationSpeed);
            }
        }
    }
}
