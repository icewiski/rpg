/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// Copyright (c) 2014 Robert Rouhani <robert.rouhani@gmail.com> and other contributors (see CONTRIBUTORS file).
// Licensed under the MIT License - https://raw.github.com/Robmaister/SharpNav/master/LICENSE

using System;

namespace SharpNav.Pathfinding
{
	/// <summary>
	/// Flags representing the type of a navmesh polygon.
	/// </summary>
	[Flags]
	public enum PolygonType : byte
	{
		/// <summary>A polygon that is part of the navmesh.</summary>
		Ground = 0,

		/// <summary>An off-mesh connection consisting of two vertices.</summary>
		OffMeshConnection = 1
	}
}
