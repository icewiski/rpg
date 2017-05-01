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
	/// 2D Point with double precision
	/// </summary>
	public class Point
	{
		/// <summary>
		/// X component of point
		/// </summary>
		protected double _X;
		/// <summary>
		/// Y component of point
		/// </summary>
		protected double _Y;

		/// <summary>
		/// Initializes a new instance of a point
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public Point(double x, double y)
		{
			_X = x;
			_Y = y;
		}
	
		/// <summary>
		/// Gets or sets the X component of the point
		/// </summary>
		public double X
		{
			get { return _X; }
			set { _X = value; }
		}

		/// <summary>
		/// Gets or sets the Y component of the point
		/// </summary>
		public double Y
		{
			get { return _Y; }
			set { _Y = value; }
		}

		/// <summary>
		/// Makes a planar checks for if the points is spatially equal to another point.
		/// </summary>
		/// <param name="other">Point to check against</param>
		/// <returns>True if X and Y values are the same</returns>
		public bool Equals2D(Point other)
		{
			return (X == other.X && Y == other.Y);
		}
	}
}
