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

namespace DungeonArchitect.Builders.SimpleCity
{
    public class RoadBeautifier {
        public static string GetRoadMarkerName(int x, int z, SimpleCityCell[,] cells, out float angle)
        {
            angle = 0;

            if (MatchesConfig(x, z, cells, out angle,
                1, 0,
                0, 1,
                -1, 0,
                0, -1
            )) return SimpleCityDungeonConstants.Road_X;

            if (MatchesConfig(x, z, cells, out angle,
                1, 0, 
                0, 1,
                -1, 0
            )) return SimpleCityDungeonConstants.Road_T;

            if (MatchesConfig(x, z, cells, out angle,
                1, 0, 
                0, 1 
            )) return SimpleCityDungeonConstants.Road_Corner;

            if (MatchesConfig(x, z, cells, out angle,
                1, 0,
                -1, 0
            )) return SimpleCityDungeonConstants.Road_S;

            return SimpleCityDungeonConstants.Road;
        }

        static bool MatchesConfig(int x, int z, SimpleCityCell[,] cells, out float angle, params int[] neighbors)
        {
            angle = 0;
            for (var da = 0; da < 4; da++)
            {
                bool rejected = false;
                for (int i = 0; i + 1 < neighbors.Length; i += 2)
                {
                    var dirX = neighbors[i];
                    var dirZ = neighbors[i + 1];
                    var direction = new Vector3(dirX, 0, dirZ);
                    direction = Quaternion.Euler(0, da * 90, 0) * direction;

                    var nx = Mathf.RoundToInt(x + direction.x);
                    var nz = Mathf.RoundToInt(z + direction.z);

                    if (!ContainsRoad(nx, nz, cells))
                    {
                        rejected = true;
                        break;
                    }
                }
                if (!rejected)
                {
                    angle = da * 90;
                    return true;
                }
            }
            return false;   // Not found
        }

        static bool ContainsRoad(int x, int z, SimpleCityCell[,] cells)
        {
            var lx = cells.GetLength(0);
            var lz = cells.GetLength(1);
            if (x < 0 || z < 0 || x >= lx || z >= lz)
            {
                return false;
            }
            return cells[x, z].CellType == SimpleCityCellType.Road;
        }
    }
}
