using UnityEngine;

namespace Assets.Code
{
    public class ProjectileWeapon : BasicWeapon
    {
        public Projectile Prefab;

        public float
            Speed,
            Damage,
            FireRate,
            TimeToLive;

        private float _cooldown;

        public override void Fire(Vector3 position, Vector3 direction)
        {
            if ((_cooldown -= Time.deltaTime) >0)
                return;

            var projectile = (Projectile) Instantiate(Prefab, position, Quaternion.identity);
            projectile.Init(this, direction);

            _cooldown = FireRate;
        }
    }
}
