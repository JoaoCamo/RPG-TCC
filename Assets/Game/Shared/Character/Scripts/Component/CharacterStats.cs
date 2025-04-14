using Game.Character.Enum;

namespace Game.Character
{
    public class CharacterStats
    {
        public int level;
        public int strength;
        public int dexterity;
        public int constitution;
        public int intelligence;
        public int wisdom;
        public int charisma;

        public void AddStat(StatsType type, int value)
        {
            switch (type)
            {
                case StatsType.Strength:
                    strength += value;
                    break;
                case StatsType.Dexterity:
                    dexterity += value;
                    break;
                case StatsType.Constitution:
                    constitution += value;
                    break;
                case StatsType.Intelligence:
                    intelligence += value;
                    break;
                case StatsType.Wisdom:
                    wisdom += value;
                    break;
                case StatsType.Charisma:
                    charisma += value;
                    break;
            }
        }
    }
}