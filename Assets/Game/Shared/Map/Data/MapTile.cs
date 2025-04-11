using UnityEngine;
using Game.Map.Enum;

namespace Game.Map.Data
{
    [CreateAssetMenu(menuName = "Map/Tile")]
    public class MapTile : ScriptableObject
    {
        public TileConnection[] connections;
        public Sprite tileSprite;
    }
}