using System.Collections.Generic;
using Game.Character.Enemy;
using Game.Item;
using Game.Map.Data;

namespace Game.Map
{
    public class MapSection
    {
        private MapTile _sectionInfo;
        private EnemyController[] _tileEnemies;
        private readonly List<ItemBase> _sectionItems = new List<ItemBase>();
        private string _roomDescription;
        private bool _isVisited = false;
        private bool _isObjective = false;

        public MapTile SectionInfo { get { return _sectionInfo; } set { _sectionInfo = value; } }
        public EnemyController[] TileEnemies { get { return _tileEnemies;} set { _tileEnemies = value;} }
        public List<ItemBase> SectionItems => _sectionItems;
        public string RoomDescription { get { return _roomDescription; } set { _roomDescription = value; } }
        public bool IsVisited { get { return _isVisited; } set { _isVisited = value; } }
        public bool isObjective { get { return _isObjective; } set { _isObjective = value; } }

        public MapSection()
        {
            _tileEnemies = new EnemyController[0];
            _roomDescription = string.Empty;
        }
    }
}