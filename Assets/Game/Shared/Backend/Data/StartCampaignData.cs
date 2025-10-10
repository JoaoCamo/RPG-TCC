namespace Game.Backend.Data
{
    [System.Serializable]
    public struct StartCampaignData
    {
        public string type;
        public string name;
        public string characterHistory;
        public string context;

        public StartCampaignData(string type, string name, string characterHistory, string context)
        {
            this.type = type;
            this.name = name;
            this.characterHistory = characterHistory;
            this.context = context;
        }
    }
}