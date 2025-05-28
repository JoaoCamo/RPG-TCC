using Game.Item.Data;
using Game.Static;

namespace Game.Item
{
    public class WeaponBase : ItemBase
    {
        private WeaponData _weaponData;

        public WeaponData WeaponData => _weaponData;

        public WeaponBase()
        {
            _weaponData.rawDamage = 1;
            _weaponData.dicesToRoll = 2;
        }

        public override void SetInfoItem(WeaponData weaponData)
        {
            _weaponData = weaponData;
        }

        public override void UseItem()
        {
            StaticVariables.PlayerController.Equipment.EquipItem(this);
        }
    }
}