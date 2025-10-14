using System;
using Game.Item;
using Game.Character.Enum;
using Game.Character.Player;
using Game.Item.Data;
using Game.Item.Enum;

namespace Game.Shared.Item.Scripts.Systems
{
    public static class ClassKit
    {
        public static void GiveClassKit(PlayerController playerController)
        {
            foreach (ItemBase item in GetItems(playerController.Class))
                playerController.Inventory.AddItem(item);
        }

        private static ItemBase[] GetItems(ClassType classType)
        {
            return classType switch
            {
                ClassType.Fighter => FighterKit(),
                ClassType.Ranger => RangerKit(),
                ClassType.Barbarian => BarbarianKit(),
                ClassType.Rogue => RogueKit(),
                ClassType.Mage => MageKit(),
                ClassType.Cleric => ClericKit(),
                _ => Array.Empty<ItemBase>()
            };
        }

        private static ItemBase[] FighterKit()
        {
            ItemData weaponItemData = new ItemData(ItemType.Weapon, "Rusty iron sword", "A worn but reliable sword", 5, 10, ItemRarity.Common);
            WeaponData weaponData = new WeaponData(2, 2, StatsType.Strength, Array.Empty<ItemBonus>());
            WeaponBase weapon = new WeaponBase();
            weapon.ItemData = weaponItemData;
            weapon.SetInfoItem(weaponData);

            ItemData armorItemData = new ItemData(ItemType.Armor, "Old iron cuirass", "Quite rusty, but better than nothing", 5, 10, ItemRarity.Common);
            ArmorData armorData = new ArmorData(14, ArmorType.Chest, ArmorClass.Heavy);
            ArmorBase armor = new ArmorBase();
            armor.ItemData = armorItemData;
            armor.SetInfoItem(armorData);

            return new ItemBase[] { weapon, armor };
        }

        private static ItemBase[] RangerKit()
        {
            ItemData weaponItemData = new ItemData(ItemType.Weapon, "Hunting Bow", "A lightweight bow, ideal for striking enemies from afar with precision.", 3, 12, ItemRarity.Common);
            WeaponData weaponData = new WeaponData(1, 3, StatsType.Dexterity, Array.Empty<ItemBonus>());
            WeaponBase weapon = new WeaponBase();
            weapon.ItemData = weaponItemData;
            weapon.SetInfoItem(weaponData);

            ItemData armorItemData = new ItemData(ItemType.Armor, "Studded Leather Armor", "Flexible armor that allows swift movements while offering basic protection.", 4, 15, ItemRarity.Common);
            ArmorData armorData = new ArmorData(12, ArmorType.Chest, ArmorClass.Medium);
            ArmorBase armor = new ArmorBase();
            armor.ItemData = armorItemData;
            armor.SetInfoItem(armorData);

            return new ItemBase[] { weapon, armor };
        }

        private static ItemBase[] BarbarianKit()
        {
            ItemData weaponItemData = new ItemData(ItemType.Weapon, "Battle Axe", "A heavy axe designed to crush armor and foes alike.", 8, 20, ItemRarity.Common);
            WeaponData weaponData = new WeaponData(4, 2, StatsType.Strength, Array.Empty<ItemBonus>());
            WeaponBase weapon = new WeaponBase();
            weapon.ItemData = weaponItemData;
            weapon.SetInfoItem(weaponData);

            ItemData armorItemData = new ItemData(ItemType.Armor, "Fur Vest", "Provides minimal protection but doesn’t hinder your rage fueled attacks.", 6, 12, ItemRarity.Common);
            ArmorData armorData = new ArmorData(10, ArmorType.Chest, ArmorClass.Light);
            ArmorBase armor = new ArmorBase();
            armor.ItemData = armorItemData;
            armor.SetInfoItem(armorData);

            return new ItemBase[] { weapon, armor };
        }

        private static ItemBase[] RogueKit()
        {
            ItemData weaponItemData = new ItemData(ItemType.Weapon, "Daggers", "A pair of sharp daggers, perfect for quick and silent strikes.", 2, 10, ItemRarity.Common);
            WeaponData weaponData = new WeaponData(1, 4, StatsType.Dexterity, Array.Empty<ItemBonus>());
            WeaponBase weapon = new WeaponBase();
            weapon.ItemData = weaponItemData;
            weapon.SetInfoItem(weaponData);

            ItemData armorItemData = new ItemData(ItemType.Armor, "Dark Leather Armor", "Light armor that allows you to move unseen and unhindered.", 3, 15, ItemRarity.Common);
            ArmorData armorData = new ArmorData(10, ArmorType.Chest, ArmorClass.Light);
            ArmorBase armor = new ArmorBase();
            armor.ItemData = armorItemData;
            armor.SetInfoItem(armorData);

            return new ItemBase[] { weapon, armor };
        }

        private static ItemBase[] MageKit()
        {
            ItemData weaponItemData = new ItemData(ItemType.Weapon, "Apprentice Staff", "A simple staff infused with magical potential.", 4, 15, ItemRarity.Common);
            WeaponData weaponData = new WeaponData(2, 2, StatsType.Intelligence, Array.Empty<ItemBonus>());
            WeaponBase weapon = new WeaponBase();
            weapon.ItemData = weaponItemData;
            weapon.SetInfoItem(weaponData);

            ItemData armorItemData = new ItemData(ItemType.Armor, "Cloth Robe", "Offers little protection but allows for unrestricted spellcasting.", 2, 10, ItemRarity.Common);
            ArmorData armorData = new ArmorData(8, ArmorType.Chest, ArmorClass.Cloth);
            ArmorBase armor = new ArmorBase();
            armor.ItemData = armorItemData;
            armor.SetInfoItem(armorData);

            return new ItemBase[] { weapon, armor };
        }

        private static ItemBase[] ClericKit()
        {
            ItemData weaponItemData = new ItemData(ItemType.Weapon, "Mace", "A sturdy mace, perfect for smashing enemies and delivering divine justice.", 5, 15, ItemRarity.Common);
            WeaponData weaponData = new WeaponData(3, 2, StatsType.Wisdom, Array.Empty<ItemBonus>());
            WeaponBase weapon = new WeaponBase();
            weapon.ItemData = weaponItemData;
            weapon.SetInfoItem(weaponData);

            ItemData armorItemData = new ItemData(ItemType.Armor, "Chainmail Vest", "Provides solid protection while allowing for moderate mobility.", 7, 20, ItemRarity.Common);
            ArmorData armorData = new ArmorData(12, ArmorType.Chest, ArmorClass.Medium);
            ArmorBase armor = new ArmorBase();
            armor.ItemData = armorItemData;
            armor.SetInfoItem(armorData);

            return new ItemBase[] { weapon, armor };
        }
    }
}