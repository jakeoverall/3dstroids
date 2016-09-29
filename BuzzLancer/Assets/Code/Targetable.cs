using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code
{
    public class Targetable : MonoBehaviour
    {
        private const float BaseOffScreenSize = 16f;
        private const float BaseMaxSize = 128f;
        private const float BaseMinSize = 100f;
        private const float SmoothMovement = 25f;

        private Vector3
            _screenPosition,
            _currentPosition,
            _offScreenPosition,
            _targetPosition;

        private float
            _currentSize,
            _targetSize;

        private bool _isOffScreen;

        public void Update()
        {
            _isOffScreen = false;

            _offScreenPosition = _screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            var distance = Vector3.Distance(Camera.main.transform.position, transform.position);

            /*Adjustable Target size */
            _targetSize = Mathf.Clamp(BaseMaxSize/distance, BaseMinSize, BaseMaxSize);

            _targetPosition.x = _screenPosition.x - _currentSize / 2f;
            _targetPosition.y = Screen.height - _screenPosition.y - _currentSize / 2f;

            _offScreenPosition.x = _screenPosition.x - BaseOffScreenSize / 2f;
            _offScreenPosition.y = Screen.height - _screenPosition.y - BaseOffScreenSize / 2f;

            if (_screenPosition.z < 0)
            {
                _isOffScreen = true;
                _offScreenPosition.x = _screenPosition.x < Screen.width/2f
                    ? BaseOffScreenSize/2f
                    : Screen.width - BaseOffScreenSize*2;

                _offScreenPosition.y = _screenPosition.y < Screen.height / 2f
                    ? BaseOffScreenSize / 2f
                    : Screen.height - BaseOffScreenSize * 2;
            }

            if (_offScreenPosition.x < 0)
            {
                _isOffScreen = true;
                _offScreenPosition.x = BaseOffScreenSize/2f;

            }
            else if (_currentPosition.x > Screen.width - BaseOffScreenSize)
            {
                _isOffScreen = true;
                _offScreenPosition.x = Screen.width - BaseOffScreenSize*2;

            }

            if (_offScreenPosition.y < 0)
            {
                _isOffScreen = true;
                _offScreenPosition.y = BaseOffScreenSize / 2f;

            }
            else if (_currentPosition.y > Screen.height - BaseOffScreenSize)
            {
                _isOffScreen = true;
                _offScreenPosition.y = Screen.height - BaseOffScreenSize * 2;

            }

            _currentSize = Mathf.Lerp(_currentSize, _targetSize, Time.deltaTime*4);

            _currentPosition.x = Mathf.Lerp(_currentPosition.x, _targetPosition.x, SmoothMovement*Time.deltaTime);
            _currentPosition.y = Mathf.Lerp(_currentPosition.y, _targetPosition.y, SmoothMovement * Time.deltaTime);
        }

        public void OnGUI()
        {
            var oldColor = GUI.color;
            GUI.color = Color.yellow;

            if (_isOffScreen)
            {
                GUI.DrawTexture(
                    new Rect(_offScreenPosition.x, _offScreenPosition.y, BaseOffScreenSize, BaseOffScreenSize),
                    GameResources.OffScreenIndicator);
            }
            else
            {
               GUI.DrawTexture(new Rect(_currentPosition.x, _currentPosition.y, _currentSize, _currentSize),
                   GameResources.TargetSquare);
            }

        }

    }
}
