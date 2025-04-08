namespace Game.Item.Data
{
    [System.Serializable]
    public struct WeaponData
    {
        public int rawDamage;
        public int dicesToRoll;
        public ItemBonus[] itemBonus;
    }
}