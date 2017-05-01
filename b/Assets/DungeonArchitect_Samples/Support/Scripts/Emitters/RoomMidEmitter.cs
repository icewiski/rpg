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
using DungeonArchitect.Builders.Grid;

public class RoomMidEmitter : DungeonMarkerEmitter {
	
	public override void EmitMarkers(DungeonBuilder builder)
	{
		var model = builder.Model as GridDungeonModel;
		if (model == null) return;
		var config = model.Config as GridDungeonConfig;
		if (config == null) return;

		var cellSize = config.GridCellSize;
		foreach (var cell in model.Cells) {
			var bounds = cell.Bounds;
			var cx = (bounds.Location.x + bounds.Size.x / 2.0f) * cellSize.x;
			var cy = bounds.Location.y * cellSize.y;
			var cz = (bounds.Location.z + bounds.Size.z / 2.0f) * cellSize.z;
			var position = new Vector3(cx, cy, cz);
			var transform = Matrix4x4.TRS(position, Quaternion.identity, Vector3.one);
			var markerName = (cell.CellType == CellType.Room) ? "RoomCenter" : "CorridorCenter";
			builder.EmitMarker(markerName, transform, cell.Bounds.Location, cell.Id);
		}

	}


}
