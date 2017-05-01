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

namespace SharpNav
{
	/// <summary>
	/// An enum similar to <see cref="Direction"/>, but with the ability to store multiple directions.
	/// </summary>
	[Flags]
	public enum EdgeFlags : byte
	{
		/// <summary>No edges are selected.</summary>
		None = 0x0,

		/// <summary>The west edge is selected.</summary>
		West = 0x1,

		/// <summary>The north edge is selected.</summary>
		North = 0x2,

		/// <summary>The east edge is selected.</summary>
		East = 0x4,

		/// <summary>The south edge is selected.</summary>
		South = 0x8,

		/// <summary>All of the edges are selected.</summary>
		All = West | North | East | South
	}

	/// <summary>
	/// A static class with helper functions to modify instances of the <see cref="EdgeFlags"/> enum.
	/// </summary>
	public static class EdgeFlagsHelper
	{
		/// <summary>
		/// Adds an edge in a specified direction to an instance of <see cref="EdgeFlags"/>.
		/// </summary>
		/// <param name="edges">An existing set of edges.</param>
		/// <param name="dir">The direction to add.</param>
		public static void AddEdge(ref EdgeFlags edges, Direction dir)
		{
			edges |= (EdgeFlags)(1 << (int)dir);
		}

		/// <summary>
		/// Flips the set of edges in an instance of <see cref="EdgeFlags"/>.
		/// </summary>
		/// <param name="edges">An existing set of edges.</param>
		public static void FlipEdges(ref EdgeFlags edges)
		{
			edges ^= EdgeFlags.All;
		}

		/// <summary>
		/// Determines whether an instance of <see cref="EdgeFlags"/> includes an edge in a specified direction.
		/// </summary>
		/// <param name="edges">A set of edges.</param>
		/// <param name="dir">The direction to check for an edge.</param>
		/// <returns>A value indicating whether the set of edges contains an edge in the specified direction.</returns>
		public static bool IsConnected(ref EdgeFlags edges, Direction dir)
		{
			return (edges & (EdgeFlags)(1 << (int)dir)) != EdgeFlags.None;
		}

		/// <summary>
		/// Removes an edge from an instance of <see cref="EdgeFlags"/>.
		/// </summary>
		/// <param name="edges">A set of edges.</param>
		/// <param name="dir">The direction to remove.</param>
		public static void RemoveEdge(ref EdgeFlags edges, Direction dir)
		{
			edges &= (EdgeFlags)(~(1 << (int)dir));
		}
	}
}
