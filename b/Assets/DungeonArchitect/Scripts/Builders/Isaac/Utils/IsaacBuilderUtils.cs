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
    public class IsaacBuilderUtils
    {

        public static IsaacRoomTile GetTileAt(int x, int z, IsaacRoomLayout layout)
        {
            if (x < 0 || x >= layout.Tiles.GetLength(0) || z < 0 || z >= layout.Tiles.GetLength(1))
            {
                var invalidTile = new IsaacRoomTile();
                invalidTile.tileType = IsaacRoomTileType.Empty;
                return invalidTile;
            }
            return layout.Tiles[x, z];
        }


        public static bool ContainsDoorAt(int x, int z, IsaacRoom room)
        {
            return room.doorPositions.Contains(new IntVector(x, 0, z));
        }

        public static IsaacRoom GetRoom(IsaacDungeonModel model, int roomId)
        {
            foreach (var room in model.rooms)
            {
                if (room.roomId == roomId)
                {
                    return room;
                }
            }
            return null;
        }
    }
}
