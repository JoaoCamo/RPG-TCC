using Game.Character.Enemy;
using Game.Map.Data;

namespace Game.Map
{
    public class MapSection
    {
            private MapTile _sectionInfo;
            private EnemyController[] _tileEnemies;
            private bool _isVisited = false;

            public MapTile SectionInfo { get { return _sectionInfo; } set { _sectionInfo = value; } }
            public EnemyController[] TileEnemies { get { return _tileEnemies;} set { _tileEnemies = value;} }
            public bool IsVisited { get { return _isVisited; } set { _isVisited = value; } }
    }
}