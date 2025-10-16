using Game.Static;
using Game.Character;

namespace Game.Shared.Character.Scripts.Player
{
    public class PlayerExperience : CharacterExperience
    {
        private int _skillPoints = 0;
        public int SkillPoints => _skillPoints;
        
        public PlayerExperience(int currentLevel) : base(currentLevel) { }

        public override void AddXp(int value)
        {
            currentXp += value;

            if (currentXp < xpTable[currentLevel - 1])
                return;
            
            currentLevel++;
            StaticEvents.OnLevelUp();
        }
    }
}