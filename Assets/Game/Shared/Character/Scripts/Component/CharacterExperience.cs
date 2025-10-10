namespace Game.Character.Player
{
    public class CharacterExperience
    {
        private int _currentLevel;
        private int _currentXp;

        private readonly int[] _xpTable = { 300, 900, 2700, 4500, 6500, 9800, 13300, 17500, 22300, 27500, 33000, 39000, 45500, 55000, 64500, 76500, 90000, 120000, 355000 };

        public CharacterExperience(int currentLevel)
        {
            _currentLevel = currentLevel;
            _currentXp = 0;
        }

        protected virtual void AddXp(int value) { }
    }
}