using Game.Character.Enum;

namespace Game.Character.Enemy
{
    public class EnemyController : CharacterBase
    {
        private EnemySize _size;
        private EnemyHealth _health;

        public EnemySize Size { get { return _size; } set { _size = value; } }
        public EnemyHealth Health => _health;

        public override void LoadCharacter(string name, CharacterType type, int level = 1)
        {
            base.LoadCharacter(name, type, level);
            _health = new EnemyHealth();
        }
    }
}