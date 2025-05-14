using System.Collections.Generic;
using UnityEngine;
using Game.Item;
using Game.Character;
using Game.Controllers;
using Game.Static;

namespace Game.UI
{
    public class InventoryExchangeUI : MonoBehaviour
    {
        [SerializeField] private MapController mapController;
        [SerializeField] private InventoryExchangeButton inventoryExchangeButtonPrefab;
        [SerializeField] private Transform playerItemsParent;
        [SerializeField] private Transform exchangeItemsParent;
        [SerializeField] private CanvasGroup canvasGroup;

        private readonly List<InventoryExchangeButton> _itemButons = new List<InventoryExchangeButton>();

        public void ToggleUI(bool mode)
        {
            canvasGroup.alpha = mode ? 1 : 0;
            canvasGroup.interactable = mode;
            canvasGroup.blocksRaycasts = mode;

            if(mode)
                LoadContent(StaticVariables.PlayerController.Inventory, mapController.GetCurrentSection().SectionItems);
            else
                RemoveItems();
        }

        private void LoadContent(CharacterInventory playerInventory, List<ItemBase> exchangeItems)
        {
            foreach(ItemBase item in playerInventory.Items)
            {
                InventoryExchangeButton button = Instantiate(inventoryExchangeButtonPrefab, playerItemsParent);
                button.Initialize(item.ItemData.itemName, () => ItemButtonOnClick(item, true));
                _itemButons.Add(button);
            }

            foreach(ItemBase item in exchangeItems)
            {
                InventoryExchangeButton button = Instantiate(inventoryExchangeButtonPrefab, playerItemsParent);
                button.Initialize(item.ItemData.itemName, () => ItemButtonOnClick(item, false));
                _itemButons.Add(button);
            }
        }

        private void RemoveItems()
        {
            foreach(InventoryExchangeButton button in _itemButons)
                Destroy(button.gameObject);

            _itemButons.Clear();
        }

        private void ItemButtonOnClick(ItemBase itemBase, bool isInPlayerInventory)
        {
            if(isInPlayerInventory)
            {
                StaticVariables.PlayerController.Inventory.Items.Remove(itemBase);
                mapController.GetCurrentSection().SectionItems.Add(itemBase);
            }
            else
            {
                StaticVariables.PlayerController.Inventory.Items.Add(itemBase);
                mapController.GetCurrentSection().SectionItems.Remove(itemBase);
            }

            RemoveItems();
            LoadContent(StaticVariables.PlayerController.Inventory, mapController.GetCurrentSection().SectionItems);
        }
    }
}