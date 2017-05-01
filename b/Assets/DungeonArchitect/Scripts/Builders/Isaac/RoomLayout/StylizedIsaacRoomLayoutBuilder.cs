/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System.Collections;

namespace DungeonArchitect.Builders.Isaac
{
    public class StylizedIsaacRoomLayoutBuilder : IsaacRoomLayoutBuilder
    {
        public int minBrushSize = 1;
        public int maxBrushSize = 3;
        public override IsaacRoomLayout GenerateLayout(IsaacRoom room, System.Random random, int roomWidth, int roomHeight)
        {
            var doors = room.doorPositions;
            var layout = new IsaacRoomLayout();
            layout.InitializeTiles(roomWidth, roomHeight, IsaacRoomTileType.Empty);

            if (doors.Count > 1)
            {
                for (int i = 0; i < doors.Count; i++)
                {
                    for (int j = i + 1; j < doors.Count; j++)
                    {
                        var brushSize = random.Range(minBrushSize, maxBrushSize + 1);
                        ConnectDoors(layout, doors[i], doors[j], brushSize);
                    }
                }
            }
            else
            {
                var brushSize = random.Range(minBrushSize, maxBrushSize + 1);
                ConnectDoors(layout, doors[0], doors[0], brushSize);
            }

            return layout;
        }

        void ConnectDoors(IsaacRoomLayout layout, IntVector doorA, IntVector doorB, int brushSize)
        {
            var minX = Mathf.Min(doorA.x, doorB.x);
            var minZ = Mathf.Min(doorA.z, doorB.z);
            var maxX = Mathf.Max(doorA.x, doorB.x);
            var maxZ = Mathf.Max(doorA.z, doorB.z);
            var width = layout.Tiles.GetLength(0);
            var height = layout.Tiles.GetLength(1);

            minX = Mathf.Clamp(minX, 0, width - 1);
            maxX = Mathf.Clamp(maxX, 0, width - 1);
            minZ = Mathf.Clamp(minZ, 0, height - 1);
            maxZ = Mathf.Clamp(maxZ, 0, height - 1);


            for (int x = minX; x <= maxX; x++)
            {
                var doorZ = Mathf.Clamp(doorA.z, 0, height - 1);
                PaintTile(layout, x, doorZ, brushSize, IsaacRoomTileType.Floor);
            }

            for (int z = minZ; z <= maxZ; z++)
            {
                var doorX = Mathf.Clamp(doorB.x, 0, width - 1);
                PaintTile(layout, doorX, z, brushSize, IsaacRoomTileType.Floor);
            }
        }

        void PaintTile(IsaacRoomLayout layout, int x, int z, int brushSize, IsaacRoomTileType tileType)
        {
            // TODO: Implement brush size
            var w = layout.Tiles.GetLength(0);
            var h = layout.Tiles.GetLength(1);
            if (x < 0 || x >= w || z < 0 || z >= h) return;

            var sx = x - Mathf.FloorToInt(brushSize / 2.0f);
            var sz = z - Mathf.FloorToInt(brushSize / 2.0f);

            for (int dx = 0; dx < brushSize; dx++)
            {
                for (int dz = 0; dz < brushSize; dz++)
                {
                    var xx = sx + dx;
                    var zz = sz + dz;

                    SetTile(layout, xx, zz, w, h, IsaacRoomTileType.Floor);
                }
            }
        }

        void SetTile(IsaacRoomLayout layout, int x, int z, int width, int height, IsaacRoomTileType tileType)
        {
            if (x < 0 || x >= width || z < 0 || z >= height) return;
            layout.Tiles[x, z].tileType = tileType;
        }
    }
}
