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
	/// Edge made from two point indexes
	/// </summary>
	public class Edge : IEquatable<Edge>
	{
		/// <summary>
		/// Start of edge index
		/// </summary>
		public int p1;
		/// <summary>
		/// End of edge index
		/// </summary>
		public int p2;
		/// <summary>
		/// Initializes a new edge instance
		/// </summary>
		/// <param name="point1">Start edge vertex index</param>
		/// <param name="point2">End edge vertex index</param>
		public Edge(int point1, int point2)
		{
			p1 = point1; p2 = point2;
		}
		/// <summary>
		/// Initializes a new edge instance with start/end indexes of '0'
		/// </summary>
		public Edge()
			: this(0, 0)
		{
		}

		#region IEquatable<dEdge> Members

		/// <summary>
		/// Checks whether two edges are equal disregarding the direction of the edges
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals(Edge other)
		{
			return
				((this.p1 == other.p2) && (this.p2 == other.p1)) ||
				((this.p1 == other.p1) && (this.p2 == other.p2));
		}

		#endregion
	}
}
