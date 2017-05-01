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
    public abstract class SpatialConstraintGridEditor : SpatialConstraintEditor
    {
        protected int minCellHeight = 40;
        protected bool DrawGridCell(SpatialConstraintGridCell cell)
        {
            string title = GetConstraintString(cell.CellType);
            bool modified = false;
            if (GUILayout.Button(title, GUILayout.MinHeight(minCellHeight)))
            {
                cell.CellType = GetNextType(cell.CellType);
                modified = true;
            }
            return modified;
        }

        SpatialConstraintGridCellType GetNextType(SpatialConstraintGridCellType cellType)
        {
            return (SpatialConstraintGridCellType)(((int)cellType + 1) % 3);
        }

        string GetConstraintString(SpatialConstraintGridCellType type)
        {
            if (type == SpatialConstraintGridCellType.DontCare)
            {
                return "Ignore";
            }
            return type.ToString();
        }
    }
}
