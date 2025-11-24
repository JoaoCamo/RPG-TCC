namespace Game.Shared.UI.Data
{
    [System.Serializable]
    public struct ArcData
    {
        public string title;
        public string arcIntroduction;

        public ArcData(string title, string arcIntroduction)
        {
            this.title = title;
            this.arcIntroduction = arcIntroduction;
        }
    }
}