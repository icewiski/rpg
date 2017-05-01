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
using System.Collections;
using DungeonArchitect;
using DungeonArchitect.Utils;
using DungeonArchitect.Builders.Grid;

public class ClampToGroundTransformRule : TransformationRule {
	
	public override void GetTransform(PropSocket socket, DungeonModel model, Matrix4x4 propTransform, System.Random random, out Vector3 outPosition, out Quaternion outRotation, out Vector3 outScale) {
		base.GetTransform(socket, model, propTransform, random, out outPosition, out outRotation, out outScale);

		// Get the ground location at this position
        if (model is GridDungeonModel)
        {
			var gridModel = model as GridDungeonModel;
            var config = gridModel.Config as GridDungeonConfig;
            var positionWorld = Matrix.GetTranslation(ref propTransform);
			var gridCoord = MathUtils.WorldToGrid(positionWorld, config.GridCellSize);
			var cellInfo = gridModel.GetGridCellLookup(gridCoord.x, gridCoord.z);
			if (cellInfo.CellType != CellType.Unknown) {
				var cell = gridModel.GetCell(socket.cellId);
				var cellY = cell.Bounds.Location.y * config.GridCellSize.y;
				var markerY = Matrix.GetTranslation(ref propTransform).y;
				var deltaY = cellY - markerY;
				outPosition.y = deltaY;
			}
		}

	}
}
