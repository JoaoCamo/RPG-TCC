using System.Collections.Generic;
using UnityEngine;
using Game.Map.Enum;
using Game.Map.Data;
using Game.Static.Enum;

namespace Game.Map
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private MapTile[] mapTiles;
        
        private const string DifficultyKey = "GAME_DIFFICULTY";

        public MapSection[,] GenerateMap(int[] initialPosition)
        {
            int roomCount = 0;
            int[] mapSize = GetMapSizeInfo();
            MapSection[,] map = new MapSection[mapSize[0], mapSize[0]];
        
            TileConnection[] mandatoryConnections = GetMandatoryConnections(initialPosition, mapSize[0], map);
            TileConnection[] forbiddenConnections = GetForbiddenConnections(initialPosition, mapSize[0], map);
        
            MapSection section = GetMapSection(mandatoryConnections, forbiddenConnections);
            section.IsVisited = true;
        
            map[initialPosition[0],initialPosition[1]] = section;
            roomCount++;
        
            foreach(TileConnection connection in section.SectionInfo.connections)
            {
                int[] newPosition = GetNewPosition(connection, initialPosition);
        
                if(map[newPosition[0],newPosition[1]] != null)
                    continue;
        
                GenerateMap(mapSize[0], mapSize[1], map, GetNewPosition(connection, initialPosition), roomCount);
            }
        
            return map;
        }

        private void GenerateMap(int mapSize, int minNumberOfRooms, MapSection[,] currentMap, int[] position, int roomCount)
        {
            TileConnection[] mandatoryConnections = GetMandatoryConnections(position, mapSize, currentMap);
            TileConnection[] forbiddenConnections = GetForbiddenConnections(position, mapSize, currentMap);

            MapSection tile = GetMapSection(mandatoryConnections, forbiddenConnections, roomCount >= minNumberOfRooms);

            currentMap[position[0],position[1]] = tile;
            roomCount++;

            foreach(TileConnection connection in tile.SectionInfo.connections)
            {
                int[] newPosition = GetNewPosition(connection, position);

                if(currentMap[newPosition[0],newPosition[1]] != null)
                    continue;

                GenerateMap(mapSize, minNumberOfRooms, currentMap, GetNewPosition(connection, position), roomCount);
            }
        }

        private MapSection GetMapSection(TileConnection[] mandatoryConnections, TileConnection[] forbiddenConnections, bool canBeSingle = true)
        {
            List<MapTile> possibleTiles = new List<MapTile>();

            foreach (MapTile tile in mapTiles)
            {
                bool isValid = true;

                foreach (TileConnection connection in mandatoryConnections)
                    if(!CheckForConnection(tile.connections, connection))
                        isValid = false;
                
                foreach (TileConnection connection in forbiddenConnections)
                    if(CheckForConnection(tile.connections, connection))
                        isValid = false;

                if(isValid)
                    possibleTiles.Add(tile);
            }

            if(!canBeSingle)
            {
                List<MapTile> newPossibleTiles = new List<MapTile>(possibleTiles);

                foreach (MapTile tile in possibleTiles)
                {
                    if(tile.connections.Length == 1)
                        newPossibleTiles.Remove(tile);
                }

                if(newPossibleTiles.Count > 0)
                    return new MapSection() { SectionInfo = newPossibleTiles[Random.Range(0, newPossibleTiles.Count)] };
            }

            return new MapSection() { SectionInfo = possibleTiles[Random.Range(0, possibleTiles.Count)] };
        }

        private TileConnection[] GetMandatoryConnections(int[] position, int mapSize, MapSection[,] currentMap)
        {
            List<TileConnection> mandatoryConnections = new List<TileConnection>();

            if (position[1] > 0 && currentMap[position[0], position[1] - 1] != null)
            {
                if (CheckForConnection(currentMap[position[0], position[1] - 1].SectionInfo.connections, TileConnection.Right))
                    mandatoryConnections.Add(TileConnection.Left);
            }

            if (position[1] < mapSize - 1 && currentMap[position[0], position[1] + 1] != null)
            {
                if (CheckForConnection(currentMap[position[0], position[1] + 1].SectionInfo.connections, TileConnection.Left))
                    mandatoryConnections.Add(TileConnection.Right);
            }

            if (position[0] > 0 && currentMap[position[0] - 1, position[1]] != null)
            {
                if (CheckForConnection(currentMap[position[0] - 1, position[1]].SectionInfo.connections, TileConnection.Bottom))
                    mandatoryConnections.Add(TileConnection.Top);
            }

            if (position[0] < mapSize - 1 && currentMap[position[0] + 1, position[1]] != null)
            {
                if (CheckForConnection(currentMap[position[0] + 1, position[1]].SectionInfo.connections, TileConnection.Top))
                    mandatoryConnections.Add(TileConnection.Bottom);
            }

            return mandatoryConnections.ToArray();
        }


        private TileConnection[] GetForbiddenConnections(int[] position, int mapSize, MapSection[,] currentMap)
        {
            List<TileConnection> forbiddenConnections = new List<TileConnection>();

            if (position[0] == 0)
            {
                forbiddenConnections.Add(TileConnection.Top);
            }
            else
            {
                MapSection topTile = currentMap[position[0] - 1, position[1]];
                if (topTile != null && !CheckForConnection(topTile.SectionInfo.connections, TileConnection.Bottom))
                    forbiddenConnections.Add(TileConnection.Top);
            }

            if (position[0] == mapSize - 1)
            {
                forbiddenConnections.Add(TileConnection.Bottom);
            }
            else
            {
                MapSection bottomTile = currentMap[position[0] + 1, position[1]];
                if (bottomTile != null && !CheckForConnection(bottomTile.SectionInfo.connections, TileConnection.Top))
                    forbiddenConnections.Add(TileConnection.Bottom);
            }

            if (position[1] == 0)
            {
                forbiddenConnections.Add(TileConnection.Left);
            }
            else
            {
                MapSection leftTile = currentMap[position[0], position[1] - 1];
                if (leftTile != null && !CheckForConnection(leftTile.SectionInfo.connections, TileConnection.Right))
                    forbiddenConnections.Add(TileConnection.Left);
            }
            
            if (position[1] == mapSize - 1)
            {
                forbiddenConnections.Add(TileConnection.Right);
            }
            else
            {
                MapSection rightTile = currentMap[position[0], position[1] + 1];
                if (rightTile != null && !CheckForConnection(rightTile.SectionInfo.connections, TileConnection.Left))
                    forbiddenConnections.Add(TileConnection.Right);
            }


            return forbiddenConnections.ToArray();
        }

        private bool CheckForConnection(TileConnection[] tileConnections, TileConnection desiredConnection)
        {
            foreach (TileConnection connection in tileConnections)
                if(connection == desiredConnection)
                    return true;

            return false;
        }

        private int[] GetNewPosition(TileConnection direction, int[] previousPosition)
        {
            return direction switch
            {
                TileConnection.Top => new int[] { previousPosition[0] - 1, previousPosition[1]},
                TileConnection.Bottom => new int[] { previousPosition[0] + 1, previousPosition[1]},
                TileConnection.Left => new int[] { previousPosition[0], previousPosition[1] - 1},
                TileConnection.Right => new int[] { previousPosition[0], previousPosition[1] + 1},
                _ => new int[2]
            };
        }

        private int[] GetMapSizeInfo()
        {
            return (GameDifficulty)PlayerPrefs.GetInt(DifficultyKey, 1) switch
            {
                GameDifficulty.Easy => new int[] {3,2},
                GameDifficulty.Normal => new int[] {4,3},
                GameDifficulty.Hard => new int[] {5,4},
                _ => new int[] {3,2},
            };
        }
    }
}