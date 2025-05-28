using UnityEngine;
using TMPro;
using Game.Character;
using Game.Character.Player;
using Game.Static;

namespace Game.UI
{
    public class PlayerStatsUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameTextMesh;
        [SerializeField] private TextMeshProUGUI healthTextMesh;
        [SerializeField] private TextMeshProUGUI locationTextMesh;
        [SerializeField] private TextMeshProUGUI[] statsTextMeshes;
        [SerializeField] private TextMeshProUGUI[] itemStatsTextMeshes;

        private void Awake()
        {
            FillUI();
        }

        private void OnEnable()
        {
            StaticEvents.OnItemUse += FillUI;
        }

        private void OnDisable()
        {
            StaticEvents.OnItemUse -= FillUI;
        }

        public void FillUI()
        {
            nameTextMesh.text = StaticVariables.PlayerController.Name;
            UpdateHealth(StaticVariables.PlayerController.Health);
            UpdatePlayerStats(StaticVariables.PlayerController.Stats);
            UpdateItemsStats(StaticVariables.PlayerController.Equipment, StaticVariables.PlayerController.Inventory);
        }

        public void UpdateHealth(PlayerHealth healthInfo)
        {
            healthTextMesh.text = healthInfo.CurrentHealth + "/" + healthInfo.MaxHealth;
        }

        public void UpdatePlayerStats(CharacterStats stats)
        {
            statsTextMeshes[0].text = "Level: " + stats.level.ToString();
            statsTextMeshes[1].text = "Strength: " + stats.strength.ToString();
            statsTextMeshes[2].text = "Dexterity: " + stats.dexterity.ToString();
            statsTextMeshes[3].text = "Constitution: " + stats.constitution.ToString();
            statsTextMeshes[4].text = "Intelligence: " + stats.intelligence.ToString();
            statsTextMeshes[5].text = "Wisdom: " + stats.wisdom.ToString();
            statsTextMeshes[6].text = "Charisma: " + stats.charisma.ToString();
        }

        public void UpdateItemsStats(CharacterEquipment equipment, CharacterInventory inventory)
        {
            itemStatsTextMeshes[0].text = "Armor Class: " + equipment.GetTotalArmor().ToString();
            itemStatsTextMeshes[1].text = "Damage: " + equipment.Weapon.WeaponData.dicesToRoll + "d" + equipment.Weapon.WeaponData.rawDamage.ToString();
            itemStatsTextMeshes[2].text = "Gold: " + inventory.CurrentGold;
        }

        public void SetLocation(string location)
        {
            locationTextMesh.text = location;
        }
    }
}