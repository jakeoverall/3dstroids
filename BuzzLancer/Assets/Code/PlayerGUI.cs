using UnityEngine;

namespace Assets.Code
{
    public class PlayerGUI
    {
        private readonly PlayerController _controller;
        private readonly Player _player;

        private readonly ProgressBar _velocityProgressBar;
        private readonly ProgressBar _healthProgressBar;

        public float CurrentCursorSize { get; private set; }

        public PlayerGUI(Player player, PlayerController controller)
        {
            _controller = controller;
            _player = player;

            _velocityProgressBar = new ProgressBar
            {
                Size = new Vector2(250, 10),
                Position = new Vector2(10, Screen.height - 10 - 10),
                BackgroundColor = new Color(199f / 255, 231f / 255, 255f / 255),
                ForegroundColor = new Color(0f / 255, 145f / 255, 255f / 255)
            };

            _healthProgressBar = new ProgressBar
            {
                Size =
                    new Vector2(_velocityProgressBar.Position.x,
                        _velocityProgressBar.Position.y - _velocityProgressBar.Size.y - 10),

                        BackgroundColor = new Color(255f / 255, 255f / 199, 208f / 255),
                        ForegroundColor = new Color(194f / 255, 62f/ 255 , 62f / 255)
            };

            CurrentCursorSize = 20;

        }

        public void Update()
        {
            _velocityProgressBar.MaxValue = _controller.MaxVariableVelocity + _controller.AfterBurnerModifier + _controller.MinimumVelocity;
            _velocityProgressBar.Value = _controller.CurrentVelocity;

            _healthProgressBar.MaxValue = _player.MaxHealth;
            _healthProgressBar.Value = _player.Health;
        }

        public void OnGUI()
        {
            GUI.DrawTexture(new Rect(_controller.MousePosition.x - (CurrentCursorSize / 2),
                Screen.height - _controller.MousePosition.y - (CurrentCursorSize / 2),
                CurrentCursorSize,
                CurrentCursorSize),
                GameResources.TargetReticle);
        }
    }
}