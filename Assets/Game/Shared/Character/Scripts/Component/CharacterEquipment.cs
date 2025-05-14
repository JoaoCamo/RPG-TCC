using Game.Item;
using Game.Item.Enum;

namespace Game.Character
{
    public class CharacterEquipment
    {
        private ArmorBase _chestArmor = new ArmorBase();
        private ArmorBase _shield = new ArmorBase();
        private WeaponBase _weapon = new WeaponBase();

        public WeaponBase Weapon => _weapon;

        public int GetTotalArmor()
        {
            int totalArmor = 0;

            totalArmor += _chestArmor != null ? _chestArmor.ArmorData.armorValue : 0;
            totalArmor += _shield != null ? _shield.ArmorData.armorValue : 0;

            return totalArmor;
        }

        public void EquipItem(ItemBase itemBase)
        {
            switch (itemBase.ItemData.itemType)
            {
                case ItemType.Weapon:
                    _weapon = itemBase as WeaponBase;
                    break;
                case ItemType.Armor:
                    EquipArmor(itemBase as ArmorBase);
                    break;
            }
        }

        private void EquipArmor(ArmorBase armorBase)
        {
            ArmorBase armor = armorBase;

            switch (armor.ArmorData.type)
            {
                case ArmorType.Chest:
                    _chestArmor = armor;
                    break;
                case ArmorType.Shield:
                    _shield = armor;
                    break;
            }
        }

        public bool CheckForItem(ItemBase itemBase)
        {
            if(itemBase.ItemData.itemType == ItemType.Armor)
            {
                ArmorBase armor = itemBase as ArmorBase;

                return armor.Equals(_chestArmor) || armor.Equals(_shield);
            }
            else if(itemBase.ItemData.itemType == ItemType.Weapon)
            {
                WeaponBase weapon = itemBase as WeaponBase;

                return weapon.Equals(_weapon);
            }

            return false;
        }
    }
}