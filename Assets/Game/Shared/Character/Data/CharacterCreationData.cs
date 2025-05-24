using Game.Character.Enum;

namespace Game.Character.Data
{
    public struct CharacterCreationData
    {
        public int level;
        public string name;
        public EnemySize size;
        public StatsData stats;
        public int dropQuantity;
    }

    public struct StatsData
    {
        public int strength;
        public int dexterity;
        public int constitution;
        public int intelligence;
        public int wisdom;
        public int charisma;
    }

    [System.Serializable]
    public struct CharacterCreationDataWrapper
    {
        public string character;
    }
}