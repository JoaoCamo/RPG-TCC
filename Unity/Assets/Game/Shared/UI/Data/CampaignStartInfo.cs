using Game.Shared.UI.Data;

namespace Game.UI.Data
{
    [System.Serializable]
    public struct CampaignStartInfo
    {
        public string title;
        public string introduction;
        public DungeonData dungeon;
    }
}