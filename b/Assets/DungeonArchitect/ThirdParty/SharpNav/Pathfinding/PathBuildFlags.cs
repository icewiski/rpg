/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// Copyright (c) 2015 Robert Rouhani <robert.rouhani@gmail.com> and other contributors (see CONTRIBUTORS file).
// Licensed under the MIT License - https://raw.github.com/Robmaister/SharpNav/master/LICENSE

using System;

namespace SharpNav.Pathfinding
{
	/// <summary>
	/// Flags for choosing how the path is built.
	/// </summary>
	[Flags]
	public enum PathBuildFlags
	{
		/// <summary>
		/// Build normally.
		/// </summary>
		None = 0x00,

		/// <summary>
		/// Adds a vertex to the path at each polygon edge crossing, but only when the areas of the two polygons are
		/// different
		/// </summary>
		AreaCrossingVertices = 0x01,

		/// <summary>
		/// Adds a vertex to the path at each polygon edge crossing.
		/// </summary>
		AllCrossingVertices = 0x02
	}
}
