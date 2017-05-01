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
    public class SimpleCityDungeonConfig : DungeonConfig
    {
        public Vector2 CellSize = new Vector2(4, 4);

        public int minSize = 15;
        public int maxSize = 20;

        public int minBlockSize = 2;
        public int maxBlockSize = 4;

        public float biggerHouseProbability = 0;

        public int cityWallPadding = 1;
        public int cityDoorSize = 1;
    }
}

