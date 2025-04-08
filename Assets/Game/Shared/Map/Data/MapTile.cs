using Game.Character.Enemy;
using Game.Map.Enum;
using UnityEngine;

namespace Game.Map.Data
{
    [CreateAssetMenu(menuName = "Map/Tile")]
    public class MapTile : ScriptableObject
    {
        [SerializeField] private TileConnection[] connections;
        private EnemyController[] tileEnemies;

        public TileConnection[] Connections => connections;
        public EnemyController[] TileEnemies { get { return tileEnemies; } set { tileEnemies = value;} }
    }
}