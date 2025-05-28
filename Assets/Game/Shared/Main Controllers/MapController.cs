using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Game.Static;
using Game.Static.Enum;
using Game.Character;
using Game.Character.Enemy;
using Game.UI;
using Game.UI.Data;
using Game.Map;
using Game.Map.Data;
using Game.Character.Player;

namespace Game.Controllers
{
    public class MapController : MonoBehaviour
    {
        [SerializeField] private CharacterGenerator characterGenerator;
        [SerializeField] private DialogController dialogController;
        [SerializeField] private CombatController combatController;
        [SerializeField] private MapGenerator mapGenerator;
        [SerializeField] private MessageBoxUI messageUI;
        [SerializeField] private MapUI mapUI;

        private MapSection[,] _currentMap;
        private int[] _currentPosition = new int[2];

        private void Awake()
        {
            mapUI.LoadButtons(this);
        }

        public void LoadNewMap()
        {
            int mapSize = GetMapSize();

            _currentPosition[0] = Random.Range(0,mapSize);
            _currentPosition[1] = Random.Range(0,mapSize);

            _currentMap = mapGenerator.GenerateMap(_currentPosition);
            GetMapObjective();

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

        public IEnumerator MovePlayer(int xDirection, int yDirection)
        {
            _currentPosition[0] += xDirection;
            _currentPosition[1] += yDirection;

            MapSection currentSection = GetCurrentSection();

            if(!currentSection.IsVisited)
            {
                yield return StartCoroutine(LoadRoomContent());

                if (currentSection.TileEnemies.Length > 0)
                {
                    CharacterBase[] charactersToEnterCombat = new CharacterBase[currentSection.TileEnemies.Length + 1];

                    for (int i = 0; i < charactersToEnterCombat.Length - 1; i++)
                    {
                        charactersToEnterCombat[i] = currentSection.TileEnemies[i];
                    }

                    charactersToEnterCombat[charactersToEnterCombat.Length - 1] = StaticVariables.PlayerController;

                    combatController.StartCombat(charactersToEnterCombat, GetCurrentSection().isObjective);
                }
            }
            else if(GetCurrentSection().isObjective)
            {
                ShowDungeonExitOption();
            }

            currentSection.IsVisited = true;
            mapUI.UpdateMap(_currentMap, _currentPosition);
            mapUI.UpdateButtons(_currentMap, _currentPosition, GetMapSize());
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

        public MapSection GetCurrentSection()
        {
            return _currentMap[_currentPosition[0], _currentPosition[1]];
        }

        private void GetMapObjective()
        {
            bool hasSelected = false;
            int mapSize = GetMapSize();
            int[] objectivePosition = new int[2];

            do
            {
                objectivePosition[0] = Random.Range(0, mapSize);
                objectivePosition[1] = Random.Range(0, mapSize);

                bool isAtSpawn = objectivePosition[0] == _currentPosition[0] && objectivePosition[1] == _currentPosition[1];
                MapSection mapSection = _currentMap[objectivePosition[0], objectivePosition[1]];

                if (mapSection != null && !isAtSpawn)
                {
                    _currentMap[objectivePosition[0],objectivePosition[1]].isObjective = true;
                    hasSelected = true;
                }
            } while (!hasSelected);
        }

        private IEnumerator LoadRoomContent()
        {
            messageUI.RequestMessageBox("Waiting for Response");
            string url = "http://127.0.0.1:5000/generate/dungeon_room/";

            UnityWebRequest request = new UnityWebRequest(url, "POST");
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
                messageUI.RequestMessageBox(request.error, new MessageBoxButtonData(messageUI.CloseMessageBox, "Close"), new MessageBoxButtonData());
            else
            {
                string response = request.downloadHandler.text;
                DungeonRoomWrapper wrapper = JsonUtility.FromJson<DungeonRoomWrapper>(response);
                DungeonRoom dungeonRoom = JsonUtility.FromJson<DungeonRoom>(wrapper.dungeon_room);

                if (dungeonRoom.enemiesPresent.enemiesPresent)
                {
                    yield return StartCoroutine(LoadRoomEnemies(dungeonRoom));
                    messageUI.CloseMessageBox();
                }
                else
                    messageUI.CloseMessageBox();
            }
        }

        private IEnumerator LoadRoomEnemies(DungeonRoom dungeonRoom)
        {
            List<EnemyController> characters = new List<EnemyController>();

            for (int i = 0; i < dungeonRoom.enemiesPresent.enemyAmount; i++)
            {
                EnemyController enemy = new EnemyController();
                yield return StartCoroutine(characterGenerator.GenerateEnemy(enemy));
                characters.Add(enemy);
            }

            GetCurrentSection().TileEnemies = characters.ToArray();
        }

        public void ShowDungeonExitOption()
        {
            string message = "You have found your objective. " +
                                "You may leave the dungeon now, or continue fighting. If you choose to keep fighting, " +
                                "just return to this room to see this message again and exit whenever you want.";

            MessageBoxButtonData boxButtonDataExit = new MessageBoxButtonData(() =>
            {
                messageUI.CloseMessageBox();
                StaticVariables.CurrentGameState = GameState.Dialogue;

                PlayerController player = StaticVariables.PlayerController;
                player.Stats.level++;
                player.Health.CalculateHealth(10, player.Stats.level, player.Stats.constitution);

                StartCoroutine(dialogController.SendDialogToAI($"{StaticVariables.PlayerController.Name} found the dungeon objective, and now exits the dungeon."));
            }, "Exit Dungeon");

            MessageBoxButtonData boxButtonDataContinue = new MessageBoxButtonData(messageUI.CloseMessageBox, "Continue Fighting");

            messageUI.RequestMessageBox(message, boxButtonDataExit, boxButtonDataContinue);
        }
    }
}