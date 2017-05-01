/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System;
using System.Collections.Generic;
using DungeonArchitect;

namespace DungeonArchitect.Builders.Isaac
{
    [Serializable]
    public class IsaacRoom
    {
        [HideInInspector]
        public int roomId;

        [HideInInspector]
        public IntVector position;

        [HideInInspector]
        public IsaacRoomLayout layout;

        [HideInInspector]
        public List<int> adjacentRooms = new List<int>();

        [HideInInspector]
        public List<IntVector> doorPositions = new List<IntVector>();
    }

    [Serializable]
    public class IsaacDoor
    {
        [HideInInspector]
        public int roomA;

        [HideInInspector]
        public int roomB;

        [HideInInspector]
        public float ratio = 0.5f;
    }

    [Serializable]
    public class IsaacRoomLayout
    {
        [HideInInspector]
        public IsaacRoomTile[,] Tiles = new IsaacRoomTile[0, 0];

        public void InitializeTiles(int width, int height, IsaacRoomTileType tileType)
        {
            Tiles = new IsaacRoomTile[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    var tile = new IsaacRoomTile();
                    tile.tileType = tileType;
                    Tiles[x, z] = tile;
                }
            }
        }
    }

    [Serializable]
    public class IsaacRoomTile
    {
        public IsaacRoomTileType tileType;
    }

    public enum IsaacRoomTileType
    {
        Floor,
        Door,
        Empty
    }

    public class IsaacDungeonModel : DungeonModel
    {
        [HideInInspector]
        public IsaacDungeonConfig config;

        [HideInInspector]
        public IsaacRoom[] rooms;

        [HideInInspector]
        public IsaacDoor[] doors;
    }
}
