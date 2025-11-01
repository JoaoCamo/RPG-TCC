using Game.Item;
using Game.Item.Data;
using TMPro;
using UnityEngine;

namespace Game.Shared.UI.Scripts.Shop
{
    public class ShopItemInfoUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI itemNameTextMesh;
        [SerializeField] private TextMeshProUGUI itemDescriptionTextMesh;
        [SerializeField] private TextMeshProUGUI itemRarityTextMesh;
        [SerializeField] private TextMeshProUGUI itemPriceTextMesh;
        [SerializeField] private ShopWeaponInfoUI weaponInfoUI;
        [SerializeField] private ShopArmorInfoUI armorInfoUI;
        
        public void ClearItemInfo()
        {
            itemNameTextMesh.text = string.Empty;
            itemDescriptionTextMesh.text = string.Empty;
            itemRarityTextMesh.text = string.Empty;
            itemPriceTextMesh.text = string.Empty;
            weaponInfoUI.DisableUI();
            armorInfoUI.DisableUI();
        }
        
        public void SetWeaponInfo(WeaponBase weaponBase)
        {
            SetItemInfo(weaponBase.ItemData);
            armorInfoUI.DisableUI();
            weaponInfoUI.SetInfo(weaponBase.WeaponData);
        }

        public void SetArmorInfo(ArmorBase armorBase)
        {
            SetItemInfo(armorBase.ItemData);
            weaponInfoUI.DisableUI();
            armorInfoUI.SetInfo(armorBase.ArmorData);
        }

        private void SetItemInfo(ItemData itemData)
        {
            itemNameTextMesh.text = itemData.itemName;
            itemDescriptionTextMesh.text = itemData.description;
            itemRarityTextMesh.text = itemData.rarity.ToString();
            itemPriceTextMesh.text = $"{itemData.value} Gold";
        }
    }
}