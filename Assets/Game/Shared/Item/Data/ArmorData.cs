using Game.Item.Enum;

namespace Game.Item.Data
{
    [System.Serializable]
    public struct ArmorData
    {
        public int armorValue;
        public ArmorType type;
        public ArmorClass armorClass;
    }
}