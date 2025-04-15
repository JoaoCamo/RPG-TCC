using UnityEngine;
using Game.Static;
using Game.Static.Enum;
using Game.Character;
using Game.Character.Enemy;
using Game.UI;
using Game.Map;

namespace Game.Controllers
{
    public class MapController : MonoBehaviour
    {
        [SerializeField] private CombatController combatController;
        [SerializeField] private MapGenerator mapGenerator;
        [SerializeField] private MapUI mapUI;

        private MapSection[,] _currentMap;
        private int[] _currentPosition = new int[2];

        private void Awake()
        {
            mapUI.LoadButtons(this);
        }

        [ContextMenu("Testar Mapa")]
        public void LoadNewMap()
        {
            int mapSize = GetMapSize();

            _currentPosition[0] = Random.Range(0,mapSize);
            _currentPosition[1] = Random.Range(0,mapSize);

            _currentMap = mapGenerator.GenerateMap(_currentPosition);
            FillMapContent();

            mapUI.LoadMap(_currentMap);
            mapUI.UpdateMap(_currentMap, _currentPosition);
            mapUI.UpdateButtons(_currentMap, _currentPosition, mapSize);
        }

        public void LoadMap()
        {
            int mapSize = GetMapSize();
            mapUI.LoadMap(_currentMap);
            mapUI.UpdateMap(_currentMap, _currentPosition);
            mapUI.UpdateButtons(_currentMap, _currentPosition, mapSize);
        }

        public void MovePlayer(int xDirection, int yDirection)
        {
            _currentPosition[0] += xDirection;
            _currentPosition[1] += yDirection;

            MapSection currentSection = _currentMap[_currentPosition[0], _currentPosition[1]];

            if(!currentSection.IsVisited && currentSection.TileEnemies.Length > 0)
            {
                currentSection.IsVisited = true;

                CharacterBase[] charactersToEnterCombat = new CharacterBase[currentSection.TileEnemies.Length + 1];

                for (int i = 0; i < charactersToEnterCombat.Length; i++)
                {
                    charactersToEnterCombat[i] = currentSection.TileEnemies[i];
                }

                charactersToEnterCombat[charactersToEnterCombat.Length - 1] = StaticVariables.PlayerController;

                combatController.StartCombat(charactersToEnterCombat);
            }
            else
            {
                currentSection.IsVisited = true;
                mapUI.UpdateMap(_currentMap, _currentPosition);
                mapUI.UpdateButtons(_currentMap, _currentPosition, GetMapSize());
            }
        }

        private void FillMapContent()
        {
            int mapSize = GetMapSize();

            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    MapSection section = _currentMap[i,j];
                    if(section != null)
                    {
                        section.TileEnemies = new EnemyController[0];
                    }
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