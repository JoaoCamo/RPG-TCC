using System.Collections.Generic;
using System.Linq;
using Game.Item;
using Game.Item.Enum;
using Game.Static;
using Game.UI.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Game.Shared.UI.Scripts.Shop
{
    public class ShopBuyUI : MonoBehaviour
    {
        [SerializeField] private ShopItemInfoUI inventoryItemInfoUI;
        [SerializeField] private ShopItemButton shopItemButtonPrefab;
        [SerializeField] private Transform itemsParent;
        [SerializeField] private Button buyItemButton;
        [SerializeField] private TextMeshProUGUI buyItemTextMesh;
        
        private readonly List<ShopItemButton> _itemButtons = new List<ShopItemButton>();
        private ItemBase _selectedItem;

        public void ToggleUI(bool mode, ItemBase[] items, UnityAction<ItemBase> buyAction)
        {
            if (mode)
            {
                ClearSelectedItem();
                LoadContent(items, buyAction);
            }
            else
                RemoveItems();
        }
        
        public void ClearSelectedItem()
        {
            inventoryItemInfoUI.ClearItemInfo();
            buyItemButton.gameObject.SetActive(false);
        }
        
        private void LoadContent(ItemBase[] items, UnityAction<ItemBase> buyAction)
        {
            RemoveItems();

            foreach (ItemBase item in items)
            {
                ShopItemButton button = Instantiate(shopItemButtonPrefab, itemsParent);
                button.Initialize(item, () => ShopButtonAction(item, buyAction));
                button.SetItemDisplayed = StaticVariables.PlayerController.Equipment.CheckForItem(button.ItemBase);
                _itemButtons.Add(button);
            }
        }

        private void ShopButtonAction(ItemBase item, UnityAction<ItemBase> buyAction)
        {
            buyItemButton.gameObject.SetActive(true);
            buyItemTextMesh.text = "Buy Item";
            
            if (item.ItemData.itemType == ItemType.Weapon)
                inventoryItemInfoUI.SetWeaponInfo(item as WeaponBase);
            else if (item.ItemData.itemType == ItemType.Armor)
                inventoryItemInfoUI.SetArmorInfo(item as ArmorBase);
            
            _selectedItem = item;
            UpdateItems();
            
            SetBuyButtonAction(buyAction);
        }

        private void SetBuyButtonAction(UnityAction<ItemBase> buyAction)
        {
            buyItemButton.onClick.RemoveAllListeners();
            buyItemButton.onClick.AddListener(() =>
            {
                if (StaticVariables.PlayerController.Inventory.CurrentGold < _selectedItem.ItemData.value)
                {
                    StaticEvents.RequestMessageBoxUIWithOptions("Insufficient gold", new MessageBoxButtonData(() => StaticEvents.CloseMessageBoxUI(), "Close"), new MessageBoxButtonData(null, string.Empty));
                    return;
                }
                
                buyAction(_selectedItem);
                RemoveBoughtItem();
            });
        }
        
        private void UpdateItems()
        {
            foreach (ShopItemButton shopItemButton in _itemButtons)
                shopItemButton.UpdateItemStatus(shopItemButton.ItemBase == _selectedItem);
        }
        
        private void RemoveBoughtItem()
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