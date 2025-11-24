using Game.Character.Enum;

namespace Game.Character.Enemy
{
    public class EnemyController : CharacterBase
    {
        private EnemySize _size;
        private EnemyHealth _health;
        private CharacterExperience _experience;

        public EnemySize Size { get => _size; set => _size = value; }
        public EnemyHealth Health => _health;
        public CharacterExperience Experience => _experience;
        public int ChallengeRating { get; set; }

        public void LoadCharacter(string name, CharacterType type, int level = 1)
        {
            base.LoadCharacter(name, type);
            _experience = new CharacterExperience(level);
            _health = new EnemyHealth(this);
        }
    }
}