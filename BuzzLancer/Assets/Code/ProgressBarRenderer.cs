using UnityEngine;

namespace Assets.Code
{
    class ProgressBarRenderer : MonoBehaviour
    {
        private ProgressBar _bar;

        public void Init(ProgressBar bar)
        {
            _bar = bar;
        }

        public void OnGUI()
        {
            var oldColor = GUI.color;

            GUI.color = _bar.BackgroundColor;
            GUI.DrawTexture(new Rect(_bar.Position.x, _bar.Position.y, _bar.Size.x, _bar.Size.y), GameResources.Square);

            GUI.color = _bar.ForegroundColor;
            GUI.DrawTexture(new Rect(_bar.Position.x, _bar.Position.y, _bar.Value * _bar.Size.x / _bar.MaxValue, _bar.Size.y), GameResources.Square);

            GUI.color = oldColor;
        }
    }
}
