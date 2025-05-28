namespace Game.UI.Data
{
    [System.Serializable]
    public struct CampaignStartInfo
    {
        public KingdomInfo kingdom;
        public DungeonInfo dungeon;
        public string introduction;
    }

    [System.Serializable]
    public struct  KingdomInfo
    {
        public string name;
        public string description;
    }

    [System.Serializable]
    public struct DungeonInfo
    {
        public string name;
        public string description;
    }

    [System.Serializable]
    public struct CampaignStartWrapper
    {
        public string main_story;
    }
}