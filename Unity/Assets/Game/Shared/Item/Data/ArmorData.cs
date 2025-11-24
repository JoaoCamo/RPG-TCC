using Game.Item.Enum;

namespace Game.Item.Data
{
    [System.Serializable]
    public struct ArmorData
    {
        public int armorValue;
        public ArmorType type;
        public ArmorClass armorClass;

        public ArmorData(int armorValue, ArmorType type, ArmorClass armorClass)
        {
            this.armorValue = armorValue;
            this.type = type;
            this.armorClass = armorClass;
        }
    }
}