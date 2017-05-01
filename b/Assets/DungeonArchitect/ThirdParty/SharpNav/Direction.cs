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
using System.Collections.Generic;

namespace SharpNav
{
	/// <summary>
	/// A set of cardinal directions.
	/// </summary>
	public enum Direction
	{
		/// <summary>
		/// The west direction.
		/// </summary>
		West = 0,

		/// <summary>
		/// The north direction.
		/// </summary>
		North = 1,

		/// <summary>
		/// The east direction.
		/// </summary>
		East = 2,

		/// <summary>
		/// The south direction.
		/// </summary>
		South = 3
	}

	/// <summary>
	/// A set of extension methods to make using the Direction enum a lot simpler.
	/// </summary>
	public static class DirectionExtensions
	{
		private static readonly int[] OffsetsX = { -1, 0, 1, 0 };
		private static readonly int[] OffsetsY = { 0, 1, 0, -1 };

		/// <summary>
		/// Gets an X offset.
		/// </summary>
		/// <remarks>
		/// The directions cycle between the following, starting from 0: west, north, east, south.
		/// </remarks>
		/// <param name="dir">The direction.</param>
		/// <returns>The offset for the X coordinate.</returns>
		public static int GetHorizontalOffset(this Direction dir)
		{
			return OffsetsX[(int)dir];
		}

		/// <summary>
		/// Get a Y offset.
		/// </summary>
		/// <remarks>
		/// The directions cycle between the following, starting from 0: west, north, east, south.
		/// </remarks>
		/// <param name="dir">The direction.</param>
		/// <returns>The offset for the Y coordinate.</returns>
		public static int GetVerticalOffset(this Direction dir)
		{
			return OffsetsY[(int)dir];
		}

		/// <summary>
		/// Gets the next cardinal direction in clockwise order.
		/// </summary>
		/// <param name="dir">The current direction.</param>
		/// <returns>The next direction.</returns>
		public static Direction NextClockwise(this Direction dir)
		{
			switch (dir)
			{
				case Direction.West:
					return Direction.North;
				case Direction.North:
					return Direction.East;
				case Direction.East:
					return Direction.South;
				case Direction.South:
					return Direction.West;
				default:
					throw new ArgumentException("dir isn't a valid Direction.");
			}
		}

		/// <summary>
		/// Gets the next cardinal direction in counter-clockwise order.
		/// </summary>
		/// <param name="dir">The current direction.</param>
		/// <returns>The next direction.</returns>
		public static Direction NextCounterClockwise(this Direction dir)
		{
			switch (dir)
			{
				case Direction.West:
					return Direction.South;
				case Direction.South:
					return Direction.East;
				case Direction.East:
					return Direction.North;
				case Direction.North:
					return Direction.West;
				default:
					throw new ArgumentException("dir isn't a valid Direction.");
			}
		}
	}
}
