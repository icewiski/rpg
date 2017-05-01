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
using System;
using System.Collections;

/// <summary>
/// Extends System.Random with gamedev based utility functions
/// </summary>
public static class RandomExtensions {
	
	public static float NextFloat(this System.Random random)
	{
		return (float)random.NextDouble();
	}

	public static Vector3 OnUnitSphere(this System.Random random) {
		var z = (float)random.NextDouble() * 2 - 1;
		var rxy = Mathf.Sqrt(1 - z*z);
		var phi = (float)random.NextDouble() * 2 * Mathf.PI;
		var x = rxy * Mathf.Cos(phi);
		var y = rxy * Mathf.Sin(phi);
		return new Vector3(x, y, z);
	}
	
	public static float Range(this System.Random random, float a, float b) {
		return a + NextFloat(random) * (b - a);
	}

	public static int Range(this System.Random random, int a, int b) {
		return Mathf.RoundToInt(a + NextFloat(random) * (b - a));
	}

	public static float value(this System.Random random) {
		return NextFloat(random);
	}
}
