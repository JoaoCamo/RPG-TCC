using System.Collections.Generic;
using Game.Item;
using Game.Static;

namespace Game.Character
{
    public class CharacterInventory
    {
        private readonly CharacterBase _base;
        private readonly List<ItemBase> _items = new List<ItemBase>();
        private int _currentGold = 0;

        public List<ItemBase> Items => _items;
        public int CurrentGold { get => _currentGold; set => _currentGold = value; }

        public CharacterInventory(CharacterBase characterBase)
        {
            _base = characterBase;
        }

        public void AddItem(ItemBase item)
        {
            _items.Add(item);
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