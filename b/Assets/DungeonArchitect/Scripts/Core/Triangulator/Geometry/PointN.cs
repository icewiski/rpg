/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonArchitect.Triangulator.Geometry
{
	/// <summary>
	/// A point with an attribute value of type 'T'
	/// </summary>
	public class Point<T> : Point
	{
		private T _attr;
		/// <summary>
		/// Initializes a new instance of the point
		/// </summary>
		/// <param name="x">X component</param>
		/// <param name="y">Y component</param>
		/// <param name="attribute">Attribute</param>
		public Point(double x, double y, T attribute)
			: base(x, y)
		{
			_attr = attribute;
		}
		/// <summary>
		/// Initializes a new instance of the point and sets the attribute to its default value
		/// </summary>
		/// <param name="x">X component</param>
		/// <param name="y">Y component</param>
		public Point(double x, double y)
			: this(x, y, default(T))
		{
		}
		/// <summary>
		/// Gets or sets the attribute component of the point
		/// </summary>
		public T Attribute
		{
			get { return _attr; }
			set { _attr = value; }
		}

	}
}
