namespace Game.Backend.Data
{
    [System.Serializable]
    public struct CharacterRequestData
    {
        public string selectedModel;
        public int level;
        public string context;

        public CharacterRequestData(int level, string context)
        {
            this.selectedModel = UnityEngine.PlayerPrefs.GetString("SELECTED_MODEL", "gpt-4.1");
            this.level = level;
            this.context = context;
        }
    }
}