using System.Collections.Generic;
using UnityEngine;
using Game.Item;

namespace Game.Character
{
    public class CharacterInventory : MonoBehaviour
    {
        private List<ItemBase> _items = new List<ItemBase>();
        private int _maxInventoryCapacity;
        private int _currentWeight;
        private int _currentGold;

        public int MaxInventoryCapacity => _maxInventoryCapacity;
        public int CurrentWeight => _currentWeight; 
        public int CurrentGold => _currentGold;

        private void UpdateCapacity(int characterStrength)
        {
            _maxInventoryCapacity = characterStrength* 15;
        }

        public void AddItems(ItemBase[] items)
        {
            foreach (ItemBase item in items)
            {
                if(_currentWeight + item.ItemData.weight > _maxInventoryCapacity)
                    break;

                _items.Add(item);
                _currentWeight++;
            }
        }
    }
}