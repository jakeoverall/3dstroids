using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code
{
    public class Asteroid : MonoBehaviour
    {
        private const int MaxLevel = 6;
       
        private float 
            _alpha,
            _velocity;

        private Vector3 _direction;

        private AsteroidManager _asteroidManager;
        private Destroyable _destroyable;

        public int Level { get; set; }
        public float DistanceSquared { get; private set; }

        public bool IsVisible { get; private set; }
        public bool IsActive { get; private set; }

        public void Awake()
        {
            _asteroidManager = (AsteroidManager) FindObjectOfType(typeof (AsteroidManager));
            _destroyable = GetComponent<Destroyable>();
        }

        public void UpdatePlayerPosition(Vector3 playerPosition)
        {
            var point = Camera.main.WorldToViewportPoint(transform.position);
            IsVisible = point.x > -5f && point.x < 1.5f && point.y > -.5f && point.y < 1.5f && point.z > 0;

            DistanceSquared = (transform.position - playerPosition).sqrMagnitude;
        }

        public void Update()
        {
            if (_alpha < 1)
            {
                _alpha = Mathf.Lerp(_alpha, 1, Time.deltaTime * 10f);

                if (_alpha > 9.9f)
                {
                    _alpha = 1;
                }

                GetComponent<Renderer>().material.color = new Color(1,1,1,_alpha);
            } 

            transform.Translate(_direction * _velocity * Time.deltaTime);
        }

        public void Init(Vector3 position, Vector3 rotation, Vector3 direction, Vector3 scale, float velocity)
        {
            Level = (int) Mathf.Ceil((scale.magnitude)/5*(MaxLevel - 1)) + 1;
            _destroyable.MaxHealth = Level * 100;
            _destroyable.Health = _destroyable.MaxHealth;

            transform.position = position;
            transform.localEulerAngles = rotation;
            transform.localScale = scale;

            _velocity = velocity;
            _direction = direction;

            IsVisible = false;
        }

        //public void OnGUI()
        //{
        //    var position = Camera.main.WorldToScreenPoint(transform.position);
        //    GUI.Label(new Rect(position.x, Screen.height - position.y, 200, 50 ), Level.ToString() );
        //}

        public void Activate()
        {
            IsActive = true;
            gameObject.SetActive(true);
            
            _alpha = 0 + Time.deltaTime;
            GetComponent<Renderer>().material.color = new Color(1,1,1,0);
        }
        public void Deactivate()
        {
            IsActive = false;
            gameObject.SetActive(false);
            _alpha = 100 - Time.deltaTime;
        }

        public void Destroyed(GameObject from)
        {
            _asteroidManager.AsteroidDestroyed(this);
        }

        public void OnTriggerEnter(Collider collision)
        {
            var destroyable = collision.gameObject.FindComponet<Destroyable>();
            if (destroyable == null)
                return;

            destroyable.TakeDamage(Level * 50, gameObject);
        }

    }
}
