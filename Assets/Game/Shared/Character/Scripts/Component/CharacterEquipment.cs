using UnityEngine;
using Game.Item;
using Game.Item.Enum;

namespace Game.Character
{
    public class CharacterEquipment : MonoBehaviour
    {
        private ArmorBase headArmor;
        private ArmorBase chestArmor;
        private ArmorBase legsArmor;
        private ArmorBase feetArmor;
        private ArmorBase shield;
        private WeaponBase weapon;

        public WeaponBase Weapon => weapon;

        public int GetTotalArmor()
        {
            int totalArmor = 0;

            totalArmor += headArmor != null ? headArmor.ArmorData.armorValue : 0;
            totalArmor += chestArmor != null ? chestArmor.ArmorData.armorValue : 0;
            totalArmor += legsArmor != null ? legsArmor.ArmorData.armorValue : 0;
            totalArmor += feetArmor != null ? feetArmor.ArmorData.armorValue : 0;
            totalArmor += shield != null ? shield.ArmorData.armorValue : 0;

            return totalArmor;
        }

        public void EquipItem(ItemType itemType, ItemBase itemBase)
        {
            switch (itemType)
            {
                case ItemType.Weapon:
                    weapon = itemBase as WeaponBase;
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
                case ArmorType.Head:
                    headArmor = armor;
                    break;
                case ArmorType.Chest:
                    chestArmor = armor;
                    break;
                case ArmorType.Legs:
                    legsArmor = armor;
                    break;
                case ArmorType.Feet:
                    feetArmor = armor;
                    break;
                case ArmorType.Shield:
                    shield = armor;
                    break;
            }
        }
    }
}