using UnityEngine;
using TMPro;
using Game.Character;
using Game.Character.Player;

namespace Game.UI
{
    public class PlayerStatsUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameTextMesh;
        [SerializeField] private TextMeshProUGUI healthTextMesh;
        [SerializeField] private TextMeshProUGUI[] statsTextMeshes;
        [SerializeField] private TextMeshProUGUI[] itemStatsTextMeshes;

        public void FillUI(PlayerController playerInfo)
        {
            nameTextMesh.text = playerInfo.Name;
            UpdateHealth(playerInfo.Health);
            UpdatePlayerStats(playerInfo.Stats);
            UpdateItemsStats(playerInfo.Equipment, playerInfo.Inventory);
        }

        public void UpdateHealth(PlayerHealth healthInfo)
        {
            healthTextMesh.text = healthInfo.CurrentHealth + "/" + healthInfo.MaxHealth;
        }

        public void UpdatePlayerStats(CharacterStats stats)
        {
            statsTextMeshes[0].text = stats.level.ToString();
            statsTextMeshes[1].text = stats.strength.ToString();
            statsTextMeshes[2].text = stats.dexterity.ToString();
            statsTextMeshes[3].text = stats.constitution.ToString();
            statsTextMeshes[4].text = stats.intelligence.ToString();
            statsTextMeshes[5].text = stats.wisdom.ToString();
            statsTextMeshes[6].text = stats.charisma.ToString();
        }

        public void UpdateItemsStats(CharacterEquipment equipment, CharacterInventory inventory)
        {
            itemStatsTextMeshes[0].text = "Armor Class: " + equipment.GetTotalArmor().ToString();
            itemStatsTextMeshes[1].text = "Damage: " + equipment.Weapon.WeaponData.dicesToRoll + "d" + equipment.Weapon.WeaponData.rawDamage.ToString();
            itemStatsTextMeshes[2].text = "Gold: " + inventory.CurrentGold;
        }
    }
}