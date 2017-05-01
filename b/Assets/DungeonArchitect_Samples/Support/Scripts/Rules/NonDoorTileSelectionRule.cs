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

public class NonDoorTileSelectionRule : SelectorRule {
	public override bool CanSelect(PropSocket socket, Matrix4x4 propTransform, DungeonModel model, System.Random random) {
		if (model is GridDungeonModel) {
			var gridModel = model as GridDungeonModel;
			var config = gridModel.Config as GridDungeonConfig;
			var cellSize = config.GridCellSize;

			var position = Matrix.GetTranslation(ref propTransform);
			var gridPositionF = MathUtils.Divide (position, cellSize);
			var gridPosition = MathUtils.ToIntVector(gridPositionF);
			var cellInfo = gridModel.GetGridCellLookup(gridPosition.x, gridPosition.z);
			return !cellInfo.ContainsDoor;
		} else {
			return false;
		}
	}
}
