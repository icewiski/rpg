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
using System.Runtime.InteropServices;

namespace SharpNav
{
	/// <summary>
	/// References a <see cref="Span"/> within a <see cref="Heightfield"/>.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct SpanReference
	{
		private int x;
		private int y;
		private int index;

		/// <summary>
		/// Initializes a new instance of the <see cref="SpanReference"/> struct.
		/// </summary>
		/// <param name="x">The X coordinate of the <see cref="Cell"/> the <see cref="Span"/> is contained in.</param>
		/// <param name="y">The Y coordinate of the <see cref="Cell"/> the <see cref="Span"/> is contained in.</param>
		/// <param name="i">The index of the <see cref="Span"/> within the specified <see cref="Cell"/>.</param>
		public SpanReference(int x, int y, int i)
		{
			this.x = x;
			this.y = y;
			this.index = i;
		}

		/// <summary>
		/// Gets the X coordinate of the <see cref="Cell"/> that contains the referenced <see cref="Span"/>.
		/// </summary>
		public int X
		{
			get
			{
				return x;
			}
		}

		/// <summary>
		/// Gets the Y coordinate of the <see cref="Cell"/> that contains the referenced <see cref="Span"/>.
		/// </summary>
		public int Y
		{
			get
			{
				return y;
			}
		}

		/// <summary>
		/// Gets the index of the <see cref="Span"/> within the <see cref="Cell"/> it is contained in.
		/// </summary>
		public int Index
		{
			get
			{
				return index;
			}
		}
	}
}
