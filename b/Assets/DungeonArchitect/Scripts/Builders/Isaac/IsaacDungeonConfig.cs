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

namespace DungeonArchitect.Builders.Isaac
{
    public class IsaacDungeonConfig : DungeonConfig
    {
        public int minRooms;
        public int maxRooms;

        public int roomWidth = 10;
        public int roomHeight = 6;

        public Vector2 tileSize = new Vector2(1, 1);
        public Vector2 roomPadding = new Vector2(1, 1);

        public float growForwardProbablity = 0.75f;
        public float growSidewaysProbablity = 0.25f;

        public float spawnRoomBranchProbablity = 0.75f;
        public float cycleProbability = 1.0f;
    }
}
