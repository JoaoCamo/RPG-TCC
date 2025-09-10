using Game.Item.Data;

namespace Game.Item
{
    public class ItemBase
    {
        protected ItemData _itemData;

        public ItemData ItemData { get { return _itemData; } set { _itemData = value; } }

        public virtual void UseItem() {}
        
        public virtual void RemoveItem() {}

        public virtual void SetInfoItem(WeaponData weaponData) {}

        public virtual void SetInfoItem(ArmorData armorData) {}
    }
}