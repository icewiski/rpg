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
using UnityEditor;
using System.Collections;
using DungeonArchitect;
using DungeonArchitect.Constraints;
using DungeonArchitect.Constraints.Grid;

namespace DungeonArchitect.Editors
{
    [CustomEditor(typeof(SpatialConstraintGrid2x2))]
    [ConstraintEditor(typeof(SpatialConstraintGrid2x2))]
    public class SpatialConstraintGrid2x2Editor : SpatialConstraintGridEditor
    {
        public override void DrawConstraintEditor(SpatialConstraint constraint)
        {
            var constraint2x2 = constraint as SpatialConstraintGrid2x2;
            if (constraint2x2.cells == null || constraint2x2.cells[0] == null)
            {
                // gets invalidated during code change / hot-reloading
                throw new System.ApplicationException("invalid state");
            }

            var cells = constraint2x2.cells;
            for (int row = 0; row < 2; row++)
            {
                EditorGUILayout.BeginHorizontal();
                DrawGridCell(cells[row * 2 + 0]);
                DrawGridCell(cells[row * 2 + 1]);
                EditorGUILayout.EndHorizontal();
            }
        }

    }
}
