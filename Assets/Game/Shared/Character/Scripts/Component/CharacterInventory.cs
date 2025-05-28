using System.Collections.Generic;
using Game.Item;
using Game.Static;

namespace Game.Character
{
    public class CharacterInventory
    {
        private readonly CharacterBase _base;
        private List<ItemBase> _items = new List<ItemBase>();
        private int _maxInventoryCapacity = 0;
        private int _currentWeight = 0;
        private int _currentGold = 0;

        public List<ItemBase> Items => _items;
        public int MaxInventoryCapacity => _maxInventoryCapacity;
        public int CurrentWeight => _currentWeight; 
        public int CurrentGold => _currentGold;

        public CharacterInventory(CharacterBase characterBase)
        {
            _base = characterBase;
        }

        public void UpdateCapacity(int characterStrength)
        {
            _maxInventoryCapacity = characterStrength * 15;
        }

        public void AddItem(ItemBase item)
        {
            if (_currentWeight + item.ItemData.weight > _maxInventoryCapacity)
                return;

            _items.Add(item);
            _currentWeight++;
        }

        public void RemoveItem(ItemBase item)
        {
            if(_base.Equipment.CheckForItem(item))
                _base.Equipment.EquipItem(item);

            _items.Remove(item);
            StaticEvents.OnItemUse();
        }
    }
}