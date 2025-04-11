using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.Map;
using Game.Static;
using Game.Static.Enum;

namespace Game.UI
{
    public class MapUI : MonoBehaviour
    {
        [SerializeField] private MapGenerator mapGenerator;
        [SerializeField] private Image tilePrefab;
        [SerializeField] private GridLayoutGroup grid;
        [SerializeField] private TextMeshProUGUI titleTextMesh;

        public void LoadMap(bool requestNew = false)
        {
            for (int i = grid.transform.childCount - 1; i >= 0; i--)
                Destroy(grid.transform.GetChild(i).gameObject);

            if(requestNew)
            {
                MapSection[,] map = mapGenerator.GenerateMap();
                StaticVariables.CurrentMap = map;
            }

            int mapSize = GetMapSize();
            grid.cellSize = new Vector2(600/mapSize,600/mapSize);

            for(int i = 0; i < mapSize; i++)
            {
                for(int j = 0; j < mapSize; j++)
                {
                    Image tile = Instantiate(tilePrefab, grid.transform);
                    MapSection mapSection = StaticVariables.CurrentMap[i,j];
                    tile.sprite = mapSection != null ? mapSection.SectionInfo.tileSprite : null;
                    tile.enabled = mapSection != null;
                }
            }
        }

        private int GetMapSize()
        {
            return StaticVariables.GameDifficulty switch
            {
                GameDifficulty.Easy => 3,
                GameDifficulty.Normal => 4,
                GameDifficulty.Hard => 5,
                _ => 3,
            };
        }
    }
}