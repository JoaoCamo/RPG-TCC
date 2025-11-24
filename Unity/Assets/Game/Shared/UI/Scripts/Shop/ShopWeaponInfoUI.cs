using Game.Item.Data;
using TMPro;
using UnityEngine;

namespace Game.Shared.UI.Scripts.Shop
{
    public class ShopWeaponInfoUI : MonoBehaviour
    {
        [SerializeField] private GameObject weaponArea;
        [SerializeField] private TextMeshProUGUI statTextMesh;
        [SerializeField] private TextMeshProUGUI damageTextMesh;

        public void SetInfo(WeaponData weaponData)
        {
            statTextMesh.text = weaponData.modifierStat.ToString();
            damageTextMesh.text = $"{weaponData.dicesToRoll}d{weaponData.rawDamage}";
            weaponArea.SetActive(true);
        }
        
        public void DisableUI()
        {
            weaponArea.SetActive(false);
        }
    }
}