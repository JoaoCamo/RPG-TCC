using Game.Item.Enum;

namespace Game.Item.Data
{
    [System.Serializable]
    public struct ItemData
    {
        public ItemType itemType;
        public string itemName;
        public string description;
        public int weight;
        public int value;
        public ItemRarity rarity;

        public ItemData(ItemType itemType, string itemName, string description, int weight, int value, ItemRarity rarity)
        {
            this.itemType = itemType;
            this.itemName = itemName;
            this.description = description;
            this.weight = weight;
            this.value = value;
            this.rarity = rarity;
        }
    }
}
