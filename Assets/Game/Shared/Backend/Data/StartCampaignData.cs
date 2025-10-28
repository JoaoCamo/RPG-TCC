namespace Game.Backend.Data
{
    [System.Serializable]
    public struct StartCampaignData
    {
        public string selectedModel;
        public string type;
        public string name;
        public string characterHistory;
        public string context;

        public StartCampaignData(string type, string name, string characterHistory, string context)
        {
            selectedModel = UnityEngine.PlayerPrefs.GetString("SELECTED_MODEL", "gpt-4.1");
            this.type = type;
            this.name = name;
            this.characterHistory = characterHistory;
            this.context = context;
        }
    }
}