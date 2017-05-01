/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// Copyright (c) 2014-2015 Robert Rouhani <robert.rouhani@gmail.com> and other contributors (see CONTRIBUTORS file).
// Licensed under the MIT License - https://raw.github.com/Robmaister/SharpNav/master/LICENSE

namespace SharpNav.Pathfinding
{
	/// <summary>
	/// A link is formed between two polygons in a TiledNavMesh
	/// </summary>
	public class Link
	{
		/// <summary>
		/// Entity links to external entity.
		/// </summary>
		public const int External = unchecked((int)0x80000000);

		/// <summary>
		/// Doesn't link to anything.
		/// </summary>
		public const int Null = unchecked((int)0xffffffff);

		/// <summary>
		/// Gets or sets the neighbor reference (the one it's linked to)
		/// </summary>
		public int Reference { get; set; }

		/// <summary>
		/// Gets or sets the index of next link
		/// </summary>
		public int Next { get; set; }

		/// <summary>
		/// Gets or sets the index of polygon edge
		/// </summary>
		public int Edge { get; set; }

		/// <summary>
		/// Gets or sets the polygon side
		/// </summary>
		public BoundarySide Side { get; set; }

		/// <summary>
		/// Gets or sets the minimum Vector3 of the bounding box
		/// </summary>
		public int BMin { get; set; }

		/// <summary>
		/// Gets or sets the maximum Vector3 of the bounding box
		/// </summary>
		public int BMax { get; set; }
	}
}
