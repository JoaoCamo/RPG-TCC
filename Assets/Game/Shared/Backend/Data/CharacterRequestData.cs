namespace Game.Backend.Data
{
    [System.Serializable]
    public struct CharacterRequestData
    {
        public int level;
        public string context;

        public CharacterRequestData(int level, string context)
        {
            this.level = level;
            this.context = context;
        }
    }
}