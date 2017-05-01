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
	/// Triangle made from three point indexes
	/// </summary>
	public struct Triangle
	{
		/// <summary>
		/// First vertex index in triangle
		/// </summary>
		public int p1;
		/// <summary>
		/// Second vertex index in triangle
		/// </summary>
		public int p2;
		/// <summary>
		/// Third vertex index in triangle
		/// </summary>
		public int p3;
		/// <summary>
		/// Initializes a new instance of a triangle
		/// </summary>
		/// <param name="point1">Vertex 1</param>
		/// <param name="point2">Vertex 2</param>
		/// <param name="point3">Vertex 3</param>
		public Triangle(int point1, int point2, int point3)
		{
			p1 = point1; p2 = point2; p3 = point3;
		}
	}
}
