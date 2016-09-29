using UnityEngine;

namespace Assets.Code
{
    public class BasicWeaponMount : MonoBehaviour
    {
        private BasicWeapon _weapon;

        public void Equip(BasicWeapon weaponPrefab)
        {
            if (_weapon != null)
                Destroy(_weapon.gameObject);

            _weapon = (BasicWeapon)Instantiate(weaponPrefab);
        }

        public void Fire(Vector3 direction)
        {
            _weapon.Fire(transform.position, direction);
        }
    }
}
