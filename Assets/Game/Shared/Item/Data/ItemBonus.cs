using Game.Character.Enum;

namespace Game.Item.Data
{
    [System.Serializable]
    public struct ItemBonus
    {
        public StatsType bonusType;
        public int bonusAmount;
    }
}