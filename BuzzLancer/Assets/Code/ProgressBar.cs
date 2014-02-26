using UnityEngine;

namespace Assets.Code
{
    class ProgressBar
    {
        private readonly GameObject _gameObject;

        private float _value;

        public Color BackgroundColor {get; set;}
        public Color ForegroundColor {get; set;}

        public float MaxValue {get; set;}

        public Vector2 Position {get; set;}
        public Vector2 Size {get; set;}

        public bool IsEnabled {get {return _gameObject.activeSelf;} set {_gameObject.SetActive(value);}}

        public float Value
        {
            get { return _value;}
            set {_value = Mathf.Clamp(value, 0, MaxValue);}
        }

        public ProgressBar()
        {
            _gameObject = new GameObject();
            _gameObject.AddComponent<ProgressBarRenderer>().Init(this);
        }

        public void Destroy()
        {
            Object.Destroy(_gameObject);
        }
    }
}
