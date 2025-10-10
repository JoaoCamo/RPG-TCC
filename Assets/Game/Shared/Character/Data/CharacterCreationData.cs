using Game.Character.Enum;

namespace Game.Character.Data
{
    [System.Serializable]
    public struct CharacterCreationData
    {
        public int level;
        public string name;
        public EnemySize size;
        public StatsData stats;
        public int dropQuantity;
    }

    [System.Serializable]
    public struct StatsData
    {
        public int strength;
        public int dexterity;
        public int constitution;
        public int intelligence;
        public int wisdom;
        public int charisma;
    }
}