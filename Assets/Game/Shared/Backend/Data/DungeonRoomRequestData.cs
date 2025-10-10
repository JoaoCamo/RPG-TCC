namespace Game.Backend.Data
{
    [System.Serializable]
    public struct DungeonRoomRequestData
    {
        public string context;

        public DungeonRoomRequestData(string context)
        {
            this.context = context;
        }
    }
}