using Game.Item.Enum;

namespace Game.Item.Data
{
    [System.Serializable]
    public struct ItemData
    {
        public string itemName;
        public string description;
        public int weight;
        public int value;
        public ItemRarity rarity;
    }
}
