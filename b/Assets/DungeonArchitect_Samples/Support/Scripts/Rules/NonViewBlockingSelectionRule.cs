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
using DungeonArchitect;
using DungeonArchitect.Utils;

public class NonViewBlockingSelectionRule : SelectorRule {
	static Vector3[] validDirections = new Vector3[] {
		new Vector3(1, 0, 0),
		new Vector3(0, 0, 1),
	};

	public override bool CanSelect(PropSocket socket, Matrix4x4 propTransform, DungeonModel model, System.Random random) {
		var rotation = Matrix.GetRotation(ref socket.Transform);
		var baseDirection = new Vector3(1, 0, 0);
		var direction = rotation * baseDirection;
		foreach (var testDirection in validDirections) {
			var dot = Vector3.Dot(direction, testDirection);
			if (dot > 0.707f) return true;
		}
		return false;
	}

}
