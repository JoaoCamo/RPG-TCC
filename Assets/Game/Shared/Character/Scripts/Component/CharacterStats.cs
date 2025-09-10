using Game.Character.Enum;

namespace Game.Character
{
    public class CharacterStats
    {
        public int level = 1;
        public int strength = 0;
        public int dexterity = 0;
        public int constitution = 0;
        public int intelligence = 0;
        public int wisdom = 0;
        public int charisma = 0;

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