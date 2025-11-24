namespace Game.Character
{
    public class CharacterExperience
    {
        protected int currentLevel;
        protected int currentXp;

        protected readonly int[] xpTable = { 300, 900, 2700, 4500, 6500, 9800, 13300, 17500, 22300, 27500, 33000, 39000, 45500, 55000, 64500, 76500, 90000, 120000, 355000 };
        
        public int Level => currentLevel;

        public CharacterExperience(int currentLevel)
        {
            this.currentLevel = currentLevel;
            currentXp = 0;
        }

        public virtual void AddXp(int value) { }
    }
}