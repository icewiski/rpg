/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System.Collections.Generic;
using DungeonArchitect;
using DungeonArchitect.Utils;
using DungeonArchitect.Builders.Grid;

public class NonStairTileSelectorRule : NonViewBlockingSelectionRule
{
    public override bool CanSelect(PropSocket socket, Matrix4x4 propTransform, DungeonModel model, System.Random random)
    {
        var selected = base.CanSelect(socket, propTransform, model, random);
        if (!selected) return false;

        // Further filter near the door positions
        if (model is GridDungeonModel)
        {
            var gridModel = model as GridDungeonModel;
            var position = Matrix.GetTranslation(ref propTransform);
            var gridSize = gridModel.Config.GridCellSize;
            var x = Mathf.FloorToInt(position.x / gridSize.x);
            var z = Mathf.FloorToInt(position.z / gridSize.z);
            return !gridModel.ContainsStairAtLocation(x, z);
        }

        return false;
    }

}