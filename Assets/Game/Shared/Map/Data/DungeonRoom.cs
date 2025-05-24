namespace Game.Map.Data
{
    [System.Serializable]
    public struct DungeonRoom
    {
        public string roomDescription;
        public EnemiesPresent enemiesPresent;
    }

    [System.Serializable]
    public struct EnemiesPresent
    {
        public bool enemiesPresent;
        public int enemyAmmount;
    }

    [System.Serializable]
    public struct DungeonRoomWrapper
    {
        public string dungeon_room;
    }
}