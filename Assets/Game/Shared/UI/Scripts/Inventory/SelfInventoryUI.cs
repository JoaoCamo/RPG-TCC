using System.Collections.Generic;
using UnityEngine;
using Game.Character;
using Game.Item;
using Game.Static;

namespace Game.UI
{
    public class SelfInventoryUI : MonoBehaviour
    {
        [SerializeField] private SelfInventoryButton selfInventoryButtonPrefab;
        [SerializeField] private Transform itemsParent;
        [SerializeField] private CanvasGroup canvasGroup;

        private readonly List<SelfInventoryButton> _itemButons = new List<SelfInventoryButton>();

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

        public void LoadContent(CharacterInventory playerInventory)
        {
            foreach (ItemBase item in playerInventory.Items)
            {
                SelfInventoryButton button = Instantiate(selfInventoryButtonPrefab, itemsParent);
                button.UpdateOutline(StaticVariables.PlayerController.Equipment);
                button.Initialize(item);
                _itemButons.Add(button);
            }
        }

        public void UpdateItems()
        {
            foreach (SelfInventoryButton button in _itemButons)
                button.UpdateOutline(StaticVariables.PlayerController.Equipment);
        }

        private void RemoveItems()
        {
            foreach (SelfInventoryButton button in _itemButons)
                Destroy(button.gameObject);

            _itemButons.Clear();
        }
    }
}