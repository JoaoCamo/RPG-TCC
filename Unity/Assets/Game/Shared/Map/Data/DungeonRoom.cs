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
        public bool isEnemiesPresent;
        public int enemyAmount;
    }
}