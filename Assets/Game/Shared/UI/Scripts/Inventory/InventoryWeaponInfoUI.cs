using UnityEngine;
using TMPro;
using Game.Item.Data;

namespace Game.Shared.UI.Scripts.Inventory
{
    public class InventoryWeaponInfoUI : MonoBehaviour
    {
        [SerializeField] private GameObject weaponArea;
        [SerializeField] private TextMeshProUGUI damageTextMesh;

        public void SetInfo(WeaponData weaponData)
        {
            damageTextMesh.text = $"{weaponData.dicesToRoll}d{weaponData.rawDamage}";
            weaponArea.SetActive(true);
        }
        
        public void DisableUI()
        {
            weaponArea.SetActive(false);
        }
    }
}