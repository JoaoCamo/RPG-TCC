using Game.Character.Enum;

namespace Game.Character.Player
{
    public class PlayerController : CharacterBase
    {
        private ClassType _class;
        private PlayerHealth _health;

        public ClassType Class { get { return _class; } set { _class = value; } }
        public PlayerHealth Health => _health;

        public override void LoadCharacter(string name, CharacterType type)
        {
            base.LoadCharacter(name, type);
            _health = new PlayerHealth();
        }
    }
}