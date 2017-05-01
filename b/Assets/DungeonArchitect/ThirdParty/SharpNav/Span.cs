/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// Copyright (c) 2013-2014 Robert Rouhani <robert.rouhani@gmail.com> and other contributors (see CONTRIBUTORS file).
// Licensed under the MIT License - https://raw.github.com/Robmaister/SharpNav/master/LICENSE

using System.Runtime.InteropServices;

namespace SharpNav
{
	/// <summary>
	/// A span is a range of integers which represents a range of voxels in a <see cref="Cell"/>.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct Span
	{
		/// <summary>
		/// The lowest value in the span.
		/// </summary>
		public int Minimum;

		/// <summary>
		/// The highest value in the span.
		/// </summary>
		public int Maximum;

		/// <summary>
		/// The span area id
		/// </summary>
		public Area Area;

		/// <summary>
		/// Initializes a new instance of the <see cref="Span"/> struct.
		/// </summary>
		/// <param name="min">The lowest value in the span.</param>
		/// <param name="max">The highest value in the span.</param>
		public Span(int min, int max)
		{
			Minimum = min;
			Maximum = max;
			Area = Area.Null;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Span"/> struct.
		/// </summary>
		/// <param name="min">The lowest value in the span.</param>
		/// <param name="max">The highest value in the span.</param>
		/// <param name="area">The area flags for the span.</param>
		public Span(int min, int max, Area area)
		{
			Minimum = min;
			Maximum = max;
			Area = area;
		}

		/// <summary>
		/// Gets the height of the span.
		/// </summary>
		public int Height
		{
			get
			{
				return Maximum - Minimum;
			}
		}
	}
}
