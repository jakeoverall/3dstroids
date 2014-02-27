using UnityEngine;

namespace Assets.Code
{
    public class Projectile : MonoBehaviour
    {
        private float _timeToLive;
        private Vector3 _direction;
        private ProjectileWeapon _weapon;

        public void Init(ProjectileWeapon weapon, Vector3 direction)
        {
            transform.LookAt(direction + transform.position);

            _weapon = weapon;
            _timeToLive = weapon.TimeToLive;
            _direction = direction;
        }

        public void Update()
        {
            if ((_timeToLive -= Time.deltaTime) < 0 )
            {
                Destroy(gameObject);
                return;
            }

            transform.Translate(_direction * _weapon.Speed * Time.deltaTime, Space.World);
        }

        public void OnTriggerEnter(Collider collision)
        {
            var destroyable = collision.GetComponent<Destroyable>();

            if (destroyable == null)
                return;

            destroyable.TakeDamage(_weapon.Damage, gameObject);
            Destroy(gameObject);
        }
    }
}