using Game.Character.Enum;

namespace Game.Item.Data
{
    [System.Serializable]
    public struct WeaponData
    {
        public int rawDamage;
        public int dicesToRoll;
        public StatsType modifierStat;
        public ItemBonus[] itemBonus;

        public WeaponData(int rawDamage, int dicesToRoll, StatsType modifierStat, ItemBonus[] itemBonus)
        {
            this.rawDamage = rawDamage;
            this.dicesToRoll = dicesToRoll;
            this.modifierStat = modifierStat;
            this.itemBonus = itemBonus;
        }
    }
}