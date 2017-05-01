/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

//$ Copyright 2016, Code Respawn Technologies Pvt Ltd - All Rights Reserved $//

using UnityEngine;
using System.Collections.Generic;
using DungeonArchitect.Graphs;

namespace DungeonArchitect.Constraints.Grid
{
    //[Meta(displayText: "2x2 (Grid)")]
    [System.Serializable]
    public class SpatialConstraintGrid2x2 : SpatialConstraint
    {
        [SerializeField]
        public SpatialConstraintGridCell[] cells = new SpatialConstraintGridCell[4];
        public override void OnEnable()
        {
            base.OnEnable();

            for (int i = 0; i < cells.Length; i++)
            {
                if (cells[i] == null)
                {
                    cells[i] = new SpatialConstraintGridCell();
                }
            }
        }
    }
}
