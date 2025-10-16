using Game.Character.Enum;
using Game.Shared.Character.Scripts.Player;

namespace Game.Character
{
    public class PlayerController : CharacterBase
    {
        private ClassType _class;
        private PlayerHealth _health;
        private PlayerExperience _experience;

        public ClassType Class { get => _class; set => _class = value; }
        public PlayerHealth Health => _health;
        public PlayerExperience Experience => _experience;

        public void LoadCharacter(string name, CharacterType type, int level = 1)
        {
            base.LoadCharacter(name, type);
            _experience = new PlayerExperience(level);
            _health = new PlayerHealth();
        }
    }
}