using UnityEngine;
using Game.Map.Enum;
using Game.Character.Enemy;

namespace Game.Map.Data
{
    [CreateAssetMenu(menuName = "Map/Tile")]
    public class MapTile : ScriptableObject
    {
        [SerializeField] private TileConnection[] connections;
        public GameObject tilePrefab;
        private EnemyController[] tileEnemies;

        public TileConnection[] Connections => connections;
        public EnemyController[] TileEnemies { get { return tileEnemies; } set { tileEnemies = value;} }
    }
}