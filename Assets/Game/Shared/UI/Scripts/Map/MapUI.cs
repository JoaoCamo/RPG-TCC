using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.Static;
using Game.Controllers;
using Game.Map;
using Game.Map.Enum;

namespace Game.UI
{
    public class MapUI : MonoBehaviour
    {
        [SerializeField] private Image tilePrefab;
        [SerializeField] private GridLayoutGroup grid;
        [SerializeField] private TextMeshProUGUI descriptionTextMesh;
        [SerializeField] private Button[] movementButtons;
        [SerializeField] private CanvasGroup canvasGroup;

        private Image[,] _mapTiles = new Image[0,0];

        public void LoadMap(MapSection[,] map)
        {
            ClearMap();

            int mapSize = map.GetLength(0);
            _mapTiles = new Image[mapSize,mapSize];

            grid.cellSize = new Vector2(600/mapSize,600/mapSize);

            for(int i = 0; i < mapSize; i++)
            {
                for(int j = 0; j < mapSize; j++)
                {
                    Image tile = Instantiate(tilePrefab, grid.transform);
                    MapSection mapSection = map[i,j];
                    tile.sprite = mapSection != null ? mapSection.SectionInfo.tileSprite : null;
                    tile.enabled = mapSection != null;
                    _mapTiles[i,j] = tile;
                }
            }
            
            StaticFunctions.ChangeCurrentUI(canvasGroup);
        }

        public void LoadButtons(MapController mapController)
        {
            movementButtons[0].onClick.AddListener(() => StartCoroutine(mapController.MovePlayer(0,-1)));
            movementButtons[1].onClick.AddListener(() => StartCoroutine(mapController.MovePlayer(1,0)));
            movementButtons[2].onClick.AddListener(() => StartCoroutine(mapController.MovePlayer(-1,0)));
            movementButtons[3].onClick.AddListener(() => StartCoroutine(mapController.MovePlayer(0,1)));
        }

        public void UpdateMap(MapSection[,] map, int[] currentPosition, string roomDescription)
        {
            int mapSize = map.GetLength(0);
            descriptionTextMesh.text = roomDescription;

            for(int i = 0; i < mapSize; i++)
            {
                for(int j = 0; j < mapSize; j++)
                {
                    MapSection mapSection = map[i,j];
                    
                    if(mapSection != null)
                    {
                        Image mapTile = _mapTiles[i,j];
                        mapTile.enabled = mapSection.IsVisited;
                        mapTile.color = i == currentPosition[0] && j == currentPosition[1] ? Color.white : Color.black;
                    }
                }
            }
        }

        public void UpdateButtons(MapSection[,] currentMap, int[] currentPosition, int mapSize)
        {
            int x = currentPosition[0];
            int y = currentPosition[1];

            movementButtons[0].interactable = y > 0 && CheckForConnection(currentMap[x,y].SectionInfo.connections, TileConnection.Left);
            movementButtons[1].interactable = x < mapSize -1 && CheckForConnection(currentMap[x,y].SectionInfo.connections, TileConnection.Bottom);
            movementButtons[2].interactable = x > 0 && CheckForConnection(currentMap[x,y].SectionInfo.connections, TileConnection.Top);
            movementButtons[3].interactable = y < mapSize - 1 && CheckForConnection(currentMap[x,y].SectionInfo.connections, TileConnection.Right);
        }

        private bool CheckForConnection(TileConnection[] tileConnections, TileConnection desiredConnection)
        {
            foreach (TileConnection connection in tileConnections)
                if(connection == desiredConnection)
                    return true;

            return false;
        }

        private void ClearMap()
        {
            int mapSize = _mapTiles.GetLength(0);

            for(int i = 0; i < mapSize; i++)
                for(int j = 0; j < mapSize; j++)
                    Destroy(_mapTiles[i,j].gameObject);

            _mapTiles = new Image[0,0];
        }
    }
}