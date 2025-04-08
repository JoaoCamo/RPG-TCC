using System.Collections.Generic;
using UnityEngine;
using Game.Map.Enum;
using Game.Map.Data;

namespace Game.Map
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private MapTile[] mapTiles;

        public MapTile[,] GenerateMap(int mapSize, int minNumberOfRooms)
        {
            int roomCount = 0;
            MapTile[,] map = new MapTile[mapSize, mapSize];
        
            int[] initialPosition =  {Random.Range(0, mapSize),Random.Range(0, mapSize)};
        
            TileConnection[] mandatoryConnections = GetMandatoryConnections(initialPosition, mapSize, map);
            TileConnection[] forbiddenConnections = GetForbiddenConnections(initialPosition, mapSize, map);
        
            MapTile tile = GetMapTile(mandatoryConnections, forbiddenConnections);
        
            map[initialPosition[0],initialPosition[1]] = tile;
            roomCount++;
        
            foreach(TileConnection connection in tile.Connections)
            {
                int[] newPosition = GetNewPosition(connection, initialPosition);
        
                if(map[newPosition[0],newPosition[1]] != null)
                    continue;
        
                GenerateMap(mapSize, minNumberOfRooms, map, GetNewPosition(connection, initialPosition), roomCount);
            }
        
            return map;
        }

        private void GenerateMap(int mapSize, int minNumberOfRooms, MapTile[,] currentMap, int[] position, int roomCount)
        {
            TileConnection[] mandatoryConnections = GetMandatoryConnections(position, mapSize, currentMap);
            TileConnection[] forbiddenConnections = GetForbiddenConnections(position, mapSize, currentMap);

            MapTile tile = GetMapTile(mandatoryConnections, forbiddenConnections, roomCount >= minNumberOfRooms);

            currentMap[position[0],position[1]] = tile;
            roomCount++;

            foreach(TileConnection connection in tile.Connections)
            {
                int[] newPosition = GetNewPosition(connection, position);

                if(currentMap[newPosition[0],newPosition[1]] != null)
                    continue;

                GenerateMap(mapSize, minNumberOfRooms, currentMap, GetNewPosition(connection, position), roomCount);
            }
        }

        private MapTile GetMapTile(TileConnection[] mandatoryConnections, TileConnection[] forbiddenConnections, bool canBeSingle = true)
        {
            List<MapTile> possibleTiles = new List<MapTile>();

            foreach (MapTile tile in mapTiles)
            {
                bool isValid = true;

                foreach (TileConnection connection in mandatoryConnections)
                    if(!CheckForConnection(tile.Connections, connection))
                        isValid = false;
                
                foreach (TileConnection connection in forbiddenConnections)
                    if(CheckForConnection(tile.Connections, connection))
                        isValid = false;

                if(isValid)
                    possibleTiles.Add(tile);
            }

            if(!canBeSingle)
            {
                List<MapTile> newPossibleTiles = new List<MapTile>(possibleTiles);

                foreach (MapTile tile in possibleTiles)
                {
                    if(tile.Connections.Length == 1)
                        newPossibleTiles.Remove(tile);
                }

                if(newPossibleTiles.Count > 0)
                    return newPossibleTiles[Random.Range(0, newPossibleTiles.Count)];
            }

            return possibleTiles[Random.Range(0, possibleTiles.Count)];
        }

        private TileConnection[] GetMandatoryConnections(int[] position, int mapSize, MapTile[,] currentMap)
        {
            List<TileConnection> mandatoryConnections = new List<TileConnection>();

            if (position[1] > 0 && currentMap[position[0], position[1] - 1] != null)
            {
                if (CheckForConnection(currentMap[position[0], position[1] - 1].Connections, TileConnection.Right))
                    mandatoryConnections.Add(TileConnection.Left);
            }

            if (position[1] < mapSize - 1 && currentMap[position[0], position[1] + 1] != null)
            {
                if (CheckForConnection(currentMap[position[0], position[1] + 1].Connections, TileConnection.Left))
                    mandatoryConnections.Add(TileConnection.Right);
            }

            if (position[0] > 0 && currentMap[position[0] - 1, position[1]] != null)
            {
                if (CheckForConnection(currentMap[position[0] - 1, position[1]].Connections, TileConnection.Bottom))
                    mandatoryConnections.Add(TileConnection.Top);
            }

            if (position[0] < mapSize - 1 && currentMap[position[0] + 1, position[1]] != null)
            {
                if (CheckForConnection(currentMap[position[0] + 1, position[1]].Connections, TileConnection.Top))
                    mandatoryConnections.Add(TileConnection.Bottom);
            }

            return mandatoryConnections.ToArray();
        }


        private TileConnection[] GetForbiddenConnections(int[] position, int mapSize, MapTile[,] currentMap)
        {
            List<TileConnection> forbiddenConnections = new List<TileConnection>();

            if (position[0] == 0)
            {
                forbiddenConnections.Add(TileConnection.Top);
            }
            else
            {
                MapTile topTile = currentMap[position[0] - 1, position[1]];
                if (topTile != null && !CheckForConnection(topTile.Connections, TileConnection.Bottom))
                    forbiddenConnections.Add(TileConnection.Top);
            }

            if (position[0] == mapSize - 1)
            {
                forbiddenConnections.Add(TileConnection.Bottom);
            }
            else
            {
                MapTile bottomTile = currentMap[position[0] + 1, position[1]];
                if (bottomTile != null && !CheckForConnection(bottomTile.Connections, TileConnection.Top))
                    forbiddenConnections.Add(TileConnection.Bottom);
            }

            if (position[1] == 0)
            {
                forbiddenConnections.Add(TileConnection.Left);
            }
            else
            {
                MapTile leftTile = currentMap[position[0], position[1] - 1];
                if (leftTile != null && !CheckForConnection(leftTile.Connections, TileConnection.Right))
                    forbiddenConnections.Add(TileConnection.Left);
            }
            
            if (position[1] == mapSize - 1)
            {
                forbiddenConnections.Add(TileConnection.Right);
            }
            else
            {
                MapTile rightTile = currentMap[position[0], position[1] + 1];
                if (rightTile != null && !CheckForConnection(rightTile.Connections, TileConnection.Left))
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
    }
}