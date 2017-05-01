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
using DungeonArchitect;
using DungeonArchitect.Utils;

namespace DungeonArchitect.Builders.SimpleCity
{

    public enum SimpleCityCellType
    {
        Road,
        House,
        House2X,
        Park,
        CityWallPadding,
        Empty
    }

    public class SimpleCityCell
    {
        public IntVector Position;
        public SimpleCityCellType CellType;
        public Quaternion Rotation;
    }

    public class SimpleCityDungeonModel : DungeonModel
    {
        [HideInInspector]
        public SimpleCityCell[,] Cells = new SimpleCityCell[0, 0];

        [HideInInspector]
        public SimpleCityCell[] WallPaddingCells;

        [HideInInspector]
        public SimpleCityDungeonConfig Config;

        [HideInInspector]
        public int CityWidth;

        [HideInInspector]
        public int CityHeight;

    }

}
