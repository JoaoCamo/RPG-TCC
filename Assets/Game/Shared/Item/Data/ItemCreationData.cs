using Game.Item.Enum;

namespace Game.Item.Data
{
    [System.Serializable]
    public struct ItemCreationData
    {
        public ItemCreationDataTypeInfo itemType;
        public string itemName;
        public string itemDescription;
        public int itemWeight;
        public int itemValue;
        public ItemRarity itemRarity;
    }

    [System.Serializable]
    public struct ItemCreationDataWrapper
    {
        public string item;
    }

    [System.Serializable]
    public struct ItemCreationDataTypeInfo
    {
        public string itemType;
        public int armorValue;
        public ArmorType armorType;
        public int rawDamage;
        public int dicesToRoll;
    }
}