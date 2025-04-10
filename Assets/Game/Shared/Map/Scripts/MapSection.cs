using Game.Map.Data;
using Game.Character.Enemy;

namespace Game.Map
{
    public class MapSection
    {
            private MapTile sectionInfo;
            private EnemyController[] tileEnemies;
            private bool isVisited = false;

            public MapTile SectionInfo { get { return sectionInfo; } set { sectionInfo = value; } }
            public EnemyController[] TileEnemies { get { return tileEnemies;} set { tileEnemies = value;} }
            public bool IsVisited { get { return isVisited; } set { isVisited = value; } }
    }
}