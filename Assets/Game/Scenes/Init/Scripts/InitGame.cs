using UnityEngine;
using UnityEngine.SceneManagement;
using Game.Character.Player;
using Game.Character.Enum;
using Game.Item;
using Game.Item.Data;
using Game.Static;
using Game.Static.Enum;

namespace Game.Scenes.Init
{
    public class InitGame : MonoBehaviour
    {
        private void Awake()
        {
            PlayerController player = new PlayerController();
            player.LoadCharacter("Durin", CharacterType.Player);

            player.Class = ClassType.Fighter;
            player.Stats.level = 1;
            player.Stats.strength = 18;
            player.Stats.dexterity = 8;
            player.Stats.constitution = 20;
            player.Stats.intelligence = 8;
            player.Stats.wisdom = 14;
            player.Stats.charisma = 10;

            player.Inventory.UpdateCapacity(player.Stats.strength);
            player.Health.CalculateHealth(10, player.Stats.level, player.Stats.constitution);

            ArmorBase armor = new ArmorBase();

            ItemData armorBaseInfo = new ItemData()
            {
                itemType = Item.Enum.ItemType.Armor,
                itemName = "Dwarven Defender Plate",
                description = "Forged in the deepest halls, this armor embodies dwarven resilience.",
                weight = 8,
                value = 200,
                rarity = Item.Enum.ItemRarity.Epic
            };

            ArmorData armorData = new ArmorData()
            {
                armorClass = Item.Enum.ArmorClass.Heavy,
                armorValue = 8,
                type = Item.Enum.ArmorType.Chest
            };

            armor.ItemData = armorBaseInfo;
            armor.SetInfoItem(armorData);

            player.Equipment.EquipItem(armor);
            player.Inventory.AddItem(armor);

            WeaponBase weapon = new WeaponBase();

            ItemData weaponBaseInfo = new ItemData()
            {
                itemType = Item.Enum.ItemType.Weapon,
                itemName = "Dwarven Warhammer",
                description = "A mighty hammer crafted with runes of the mountain, capable of shattering stone and bone.",
                weight = 7,
                value = 400,
                rarity = Item.Enum.ItemRarity.Epic
            };

            WeaponData weaponData = new WeaponData()
            {
                rawDamage = 3,
                dicesToRoll = 2,
                itemBonus = new ItemBonus[0]
            };

            weapon.ItemData = weaponBaseInfo;
            weapon.SetInfoItem(weaponData);
            player.Inventory.AddItem(weapon);

            player.Equipment.EquipItem(weapon);

            StaticVariables.PlayerController = player;
            StaticVariables.CurrentGameState = GameState.Dialogue;
            StaticVariables.GameDifficulty = GameDifficulty.Normal;

            SceneManager.LoadScene(1);
        }
    }
}