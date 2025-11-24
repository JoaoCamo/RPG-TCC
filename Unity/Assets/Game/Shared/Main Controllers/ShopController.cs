using System.Collections;
using System.Collections.Generic;
using Game.Character;
using Game.Item;
using Game.Static;
using UnityEngine;

namespace Game.Shared.Main_Controllers
{
    public class ShopController : MonoBehaviour
    {
        [SerializeField] private ItemGenerator itemGenerator;
        
        private readonly List<ItemBase> _shopItems = new List<ItemBase>();
        private bool _reloadItems = true;
        
        public bool ReloadItems { get => _reloadItems; set => _reloadItems = value; }
        public List<ItemBase> ShopItems => _shopItems;

        public IEnumerator GetItems()
        {
            StaticEvents.RequestMessageBoxUI("Generating Items");
            string jsonPlayerData = JsonUtility.ToJson(StaticVariables.PlayerController);

            _shopItems.Clear();
            
            for (int i = 0; i < 6; i++)
            {
                if (Random.value >= 0.5f)
                {
                    WeaponBase weaponBase = new WeaponBase();
                    yield return StartCoroutine(itemGenerator.GenerateItem(weaponBase, jsonPlayerData));
                    _shopItems.Add(weaponBase);
                }
                else
                {
                    ArmorBase armorBase = new ArmorBase();
                    yield return StartCoroutine(itemGenerator.GenerateItem(armorBase, jsonPlayerData));
                    _shopItems.Add(armorBase);
                }
            }

            StaticEvents.CloseMessageBoxUI();
        }
        
        public void BuyItem(ItemBase item)
        {
            CharacterInventory playerInventory = StaticVariables.PlayerController.Inventory;
            playerInventory.CurrentGold -= item.ItemData.value;
            playerInventory.AddItem(item);
            _shopItems.Remove(item);
        }

        public void SellItem(ItemBase item)
        {
            CharacterInventory playerInventory = StaticVariables.PlayerController.Inventory;
            playerInventory.CurrentGold += item.ItemData.value;
            playerInventory.RemoveItem(item);
            _shopItems.Add(item);
        }
    }
}