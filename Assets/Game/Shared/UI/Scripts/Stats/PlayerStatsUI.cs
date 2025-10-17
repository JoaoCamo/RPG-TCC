using UnityEngine;
using TMPro;
using Game.Static;
using Game.Character;
using Game.Shared.Character.Scripts.Player;

namespace Game.UI
{
    public class PlayerStatsUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameTextMesh;
        [SerializeField] private TextMeshProUGUI healthTextMesh;
        [SerializeField] private TextMeshProUGUI[] statsTextMeshes;
        [SerializeField] private TextMeshProUGUI[] itemStatsTextMeshes;

        private void Awake()
        {
            FillUI();
        }

        private void OnEnable()
        {
            StaticEvents.OnItemUse += FillUI;
            StaticEvents.OnLevelUp += FillUI;
        }

        private void OnDisable()
        {
            StaticEvents.OnItemUse -= FillUI;
            StaticEvents.OnLevelUp -= FillUI;
        }

        public void FillUI()
        {
            nameTextMesh.text = StaticVariables.PlayerController.Name;
            UpdateHealth(StaticVariables.PlayerController.Health);
            UpdatePlayerStats(StaticVariables.PlayerController.Stats, StaticVariables.PlayerController.Experience);
            UpdateItemsStats(StaticVariables.PlayerController.Equipment, StaticVariables.PlayerController.Inventory);
        }

        public void UpdateHealth(PlayerHealth healthInfo)
        {
            healthTextMesh.text = healthInfo.CurrentHealth + "/" + healthInfo.MaxHealth;
        }

        public void UpdatePlayerStats(CharacterStats stats, PlayerExperience experience)
        {
            statsTextMeshes[0].text = "Level: " + experience.Level;
            statsTextMeshes[1].text = "Strength: " + stats.strength;
            statsTextMeshes[2].text = "Dexterity: " + stats.dexterity;
            statsTextMeshes[3].text = "Constitution: " + stats.constitution;
            statsTextMeshes[4].text = "Intelligence: " + stats.intelligence;
            statsTextMeshes[5].text = "Wisdom: " + stats.wisdom;
            statsTextMeshes[6].text = "Charisma: " + stats.charisma;
        }

        public void UpdateItemsStats(CharacterEquipment equipment, CharacterInventory inventory)
        {
            itemStatsTextMeshes[0].text = "Armor Class: " + equipment.GetTotalArmor();
            itemStatsTextMeshes[1].text = "Damage: " + equipment.Weapon.WeaponData.dicesToRoll + "d" + equipment.Weapon.WeaponData.rawDamage;
            itemStatsTextMeshes[2].text = "Gold: " + inventory.CurrentGold;
        }
    }
}