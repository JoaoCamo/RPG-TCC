using System.Collections.Generic;
using UnityEngine;
using Game.Character;
using Game.Item;
using Game.Item.Enum;
using Game.Shared.UI.Scripts.Inventory;
using Game.Static;
using TMPro;
using UnityEngine.UI;

namespace Game.UI
{
    public class SelfInventoryUI : MonoBehaviour
    {
        [SerializeField] private InventoryItemInfoUI inventoryItemInfoUI;
        [SerializeField] private SelfInventoryButton selfInventoryButtonPrefab;
        [SerializeField] private Transform itemsParent;
        [SerializeField] private Button equipItemButton;
        [SerializeField] private TextMeshProUGUI equipItemButtonTextMesh;
        [SerializeField] private CanvasGroup canvasGroup;

        private readonly List<SelfInventoryButton> _itemButtons = new List<SelfInventoryButton>();
        private ItemBase _selectedItem;

        private void Awake()
        {
            equipItemButton.onClick.AddListener(EquipItemButtonOnClick);
        }

        private void OnEnable()
        {
            StaticEvents.OnItemUse += UpdateItems;
        }

        private void OnDisable()
        {
            StaticEvents.OnItemUse -= UpdateItems;
        }

        public void ToggleUI(bool mode)
        {
            canvasGroup.alpha = mode ? 1 : 0;
            canvasGroup.interactable = mode;
            canvasGroup.blocksRaycasts = mode;

            if (mode)
                LoadContent(StaticVariables.PlayerController.Inventory);
            else
                RemoveItems();
        }

        private void LoadContent(CharacterInventory playerInventory)
        {
            RemoveItems();

            foreach (ItemBase item in playerInventory.Items)
            {
                SelfInventoryButton button = Instantiate(selfInventoryButtonPrefab, itemsParent);
                button.Initialize(item, () => InventoryButtonAction(item));
                button.SetItemEquipped = StaticVariables.PlayerController.Equipment.CheckForItem(button.ItemBase);
                _itemButtons.Add(button);
            }
        }

        private void InventoryButtonAction(ItemBase item)
        {
            equipItemButton.gameObject.SetActive(true);
            
            if (item.ItemData.itemType == ItemType.Weapon)
                inventoryItemInfoUI.SetWeaponInfo(item as WeaponBase);
            else if (item.ItemData.itemType == ItemType.Armor)
                inventoryItemInfoUI.SetArmorInfo(item as ArmorBase);

            equipItemButtonTextMesh.text = StaticVariables.PlayerController.Equipment.CheckForItem(item) ? "Unequip Item" : "Equip Item";
            _selectedItem = item;
        }

        private void EquipItemButtonOnClick()
        {
            if (_selectedItem == null)
                return;
            
            _selectedItem.UseItem();
            StaticEvents.OnItemUse();
            
            equipItemButtonTextMesh.text = StaticVariables.PlayerController.Equipment.CheckForItem(_selectedItem) ? "Unequip Item" : "Equip Item";
        }

        private void UpdateItems()
        {
            foreach (SelfInventoryButton button in _itemButtons)
                button.SetItemEquipped = StaticVariables.PlayerController.Equipment.CheckForItem(button.ItemBase);
        }

        private void RemoveItems()
        {
            foreach (SelfInventoryButton button in _itemButtons)
                Destroy(button.gameObject);

            _itemButtons.Clear();
        }
    }
}