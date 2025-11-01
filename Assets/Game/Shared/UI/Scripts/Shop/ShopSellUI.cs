using System.Collections.Generic;
using System.Linq;
using Game.Item;
using Game.Item.Enum;
using Game.Static;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Shared.UI.Scripts.Shop
{
    public class ShopSellUI : MonoBehaviour
    {
        [SerializeField] private ShopItemInfoUI inventoryItemInfoUI;
        [SerializeField] private ShopItemButton shopItemButtonPrefab;
        [SerializeField] private Transform itemsParent;
        [SerializeField] private Button sellItemButton;
        [SerializeField] private TextMeshProUGUI sellItemTextMesh;
        
        private readonly List<ShopItemButton> _itemButtons = new List<ShopItemButton>();
        private ItemBase _selectedItem;

        public void ToggleUI(bool mode, UnityAction<ItemBase> sellAction)
        {
            if (mode)
            {
                ClearSelectedItem();
                LoadContent(StaticVariables.PlayerController.Inventory.Items.ToArray(), sellAction);
            }
            else
                RemoveItems();
        }
        
        public void ClearSelectedItem()
        {
            inventoryItemInfoUI.ClearItemInfo();
            sellItemButton.gameObject.SetActive(false);
        }
        
        private void LoadContent(ItemBase[] items, UnityAction<ItemBase> sellAction)
        {
            RemoveItems();

            foreach (ItemBase item in items)
            {
                ShopItemButton button = Instantiate(shopItemButtonPrefab, itemsParent);
                button.Initialize(item, () => ShopButtonAction(item, sellAction));
                _itemButtons.Add(button);
            }
        }

        private void ShopButtonAction(ItemBase item, UnityAction<ItemBase> sellAction)
        {
            sellItemButton.gameObject.SetActive(true);
            sellItemTextMesh.text = "Sell Item";
            
            if (item.ItemData.itemType == ItemType.Weapon)
                inventoryItemInfoUI.SetWeaponInfo(item as WeaponBase);
            else if (item.ItemData.itemType == ItemType.Armor)
                inventoryItemInfoUI.SetArmorInfo(item as ArmorBase);
            
            _selectedItem = item;
            UpdateItems();
            
            SetSellButtonAction(sellAction);
        }

        private void SetSellButtonAction(UnityAction<ItemBase> sellAction)
        {
            sellItemButton.onClick.RemoveAllListeners();
            sellItemButton.onClick.AddListener(() =>
            {
                sellAction(_selectedItem);
                RemoveSoldItem();
            });
        }

        private void UpdateItems()
        {
            foreach (ShopItemButton shopItemButton in _itemButtons)
                shopItemButton.UpdateItemStatus(shopItemButton.ItemBase == _selectedItem);
        }
        
        private void RemoveSoldItem()
        {
            foreach (ShopItemButton shopItemButton in _itemButtons.Where(shopItemButton => shopItemButton.ItemBase == _selectedItem))
            {
                _itemButtons.Remove(shopItemButton);
                Destroy(shopItemButton.gameObject);
                _selectedItem = null;
                break;
            }
        }
        
        private void RemoveItems()
        {
            ClearSelectedItem();
            
            foreach (ShopItemButton button in _itemButtons)
                Destroy(button.gameObject);

            _itemButtons.Clear();
        }
    }
}