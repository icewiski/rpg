/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

//$ Copyright 2016, Code Respawn Technologies Pvt Ltd - All Rights Reserved $//

using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using DungeonArchitect.Graphs;

namespace DungeonArchitect.Utils
{
    /// <summary>
    /// Various math utility functions
    /// </summary>
    public class MathUtils
    {
        /// <summary>
        /// Copies the rectangle object
        /// </summary>
        /// <param name="other">The object to copy</param>
        /// <returns>The copied object</returns>
        public static Rectangle Copy(Rectangle other)
        {
            return new Rectangle(other.X, other.Z, other.Width, other.Length);
        }

        /// <summary>
        /// Divides two vector3 objects
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>The divided vector</returns>
		public static Vector3 Divide(Vector3 a, Vector3 b) {
			return new Vector3(
				a.x / b.x,
				a.y / b.y,
				a.z / b.z
				);
		}

        /// <summary>
        /// Converts an IntVector to a Vector3
        /// </summary>
        /// <param name="v">the input int vector</param>
        /// <returns></returns>
		public static Vector3 ToVector3(IntVector v) {
			return new Vector3(v.x, v.y, v.z);
		}

        /// <summary>
        /// Converts the world coordinates to grid coordinates
        /// </summary>
        /// <param name="WorldCoord">The world cooridnate</param>
        /// <param name="GridCellSize">The grid cell size</param>
        /// <returns>The resulting grid coordinate</returns>
		public static IntVector WorldToGrid(Vector3 WorldCoord, Vector3 GridCellSize) {
			return ToIntVector(Divide (WorldCoord, GridCellSize));
		}

        /// <summary>
        /// Converts the grid coordinate to world coordinate
        /// </summary>
        /// <param name="GridCellSize">The grid cell size</param>
        /// <param name="v">The input grid coordinate</param>
        /// <returns>The resulting world coordinate</returns>
        public static Vector3 GridToWorld(Vector3 GridCellSize, IntVector v)
        {
			return GridToWorld(GridCellSize, ToVector3(v));
		}

        /// <summary>
        /// Converts the grid coordinate to world coordinate
        /// </summary>
        /// <param name="GridCellSize">The grid cell size</param>
        /// <param name="v">The input grid coordinate</param>
        /// <returns>The resulting world coordinate</returns>
        public static Vector3 GridToWorld(Vector3 GridCellSize, Vector3 v)
        {
			return Vector3.Scale (GridCellSize, v);
		}

        /// <summary>
        /// Converts an IntVector to a Vector3, with the XYZ components floored
        /// </summary>
        /// <param name="v">The input Vector3 to convert</param>
        /// <returns>The corresponding IntVector, floored in each component</returns>
		public static IntVector ToIntVector(Vector3 v) {
			return new IntVector(
				Mathf.FloorToInt(v.x),
				Mathf.FloorToInt(v.y),
				Mathf.FloorToInt(v.z)
				);
		}

        /// <summary>
        /// Rounds to an IntVector, with the XYZ components rounded to the nearest int
        /// </summary>
        /// <param name="v">The input Vector3 to convert</param>
        /// <returns>The rounded IntVector</returns>
		public static IntVector RoundToIntVector(Vector3 v) {
			return new IntVector(
				Mathf.RoundToInt(v.x),
				Mathf.RoundToInt(v.y),
				Mathf.RoundToInt(v.z)
				);
		}

        /// <summary>
        /// Snaps the position to the nearest grid cell location
        /// </summary>
        /// <param name="position">The position to snap</param>
        /// <param name="gridCellSize">The size of the grid cell</param>
        /// <returns>The snapped position</returns>
		public static Vector3 SnapToGrid(Vector3 position, Vector3 gridCellSize) {
			return SnapToGrid(position, gridCellSize, true);
		}

        /// <summary>
        /// Snaps the position to the nearest grid cell location
        /// </summary>
        /// <param name="position">The position to snap</param>
        /// <param name="gridCellSize">The size of the grid cell</param>
        /// <param name="useRounding">Flag to indicate if rounding is to be used.  Uses floor if false</param>
        /// <returns>The snapped position</returns>
        public static Vector3 SnapToGrid(Vector3 position, Vector3 gridCellSize, bool useRounding)
        {
			Vector3 gridPosition;
			if (useRounding) {
				gridPosition = new Vector3(
					Mathf.RoundToInt(position.x / gridCellSize.x),
					Mathf.RoundToInt(position.y / gridCellSize.y),
					Mathf.RoundToInt(position.z / gridCellSize.z));
			} else {
				gridPosition = new Vector3(
					Mathf.FloorToInt(position.x / gridCellSize.x),
					Mathf.FloorToInt(position.y / gridCellSize.y),
					Mathf.FloorToInt(position.z / gridCellSize.z));
				//gridPosition += Vector3.one;
			}
			return Vector3.Scale(gridPosition, gridCellSize);
		}

        /// <summary>
        /// Checks if the two rectangles intersect
        /// </summary>
        /// <param name="outer">The outer rect</param>
        /// <param name="inner">The inner rect</param>
        /// <returns>True if they intersect, false otherwise</returns>
		public static bool Intersects(Rect outer, Rect inner) {
			if (outer.Contains(inner.center)) return true;
			float w = inner.width / 2.0f;
			float h = inner.height / 2.0f;
			if (outer.Contains(inner.center + new Vector2(-w, -h))) return true;
			if (outer.Contains(inner.center + new Vector2( w, -h))) return true;
			if (outer.Contains(inner.center + new Vector2( w,  h))) return true;
			if (outer.Contains(inner.center + new Vector2(-w,  h))) return true;
			return false;
		}
		
        /// <summary>
        /// Test if the graph link lies within the rectangle
        /// </summary>
        /// <param name="outer">The rect to test against</param>
        /// <param name="link">The link to test the intersection</param>
        /// <returns>True if intersects, false otherwise</returns>
		public static bool Intersects(Rect outer, GraphLink link) {
			if (link == null || link.Input == null || link.Output == null) return false;

			var p0 = link.Input.WorldPosition;
			var p1 = link.Output.WorldPosition;

			if (outer.Contains(p0) || outer.Contains(p1)) {
				return true;
			}
			
			var x0 = outer.position.x;
			var x1 = outer.position.x + outer.size.x;
			var y0 = outer.position.y;
			var y1 = outer.position.y + outer.size.y;

			var outside = 
					(p0.x < x0 && p1.x < x0) ||
					(p0.x > x1 && p1.x > x1) ||
					(p0.y < y0 && p1.y < y0) ||
					(p0.y > y1 && p1.y > y1);

			return !outside;
		}
		
		/// <summary>
		/// Flips the coordinates for 2D mode
		/// </summary>
		/// <param name="bounds">Bounds.</param>
		public static void FlipYZ(ref Bounds bounds) {
			bounds.size = FlipYZ (bounds.size);
			var center = FlipYZ (bounds.center);
			center.y = 0;
			bounds.center = center;
		}

		/// <summary>
		/// Flips the coordinates for 2D mode
		/// </summary>
		/// <returns>The Y.</returns>
		/// <param name="bounds">Bounds.</param>
		public static Vector3 FlipYZ(Vector3 bounds) {
			var z = bounds.z;
			bounds.z = bounds.y;
			bounds.y = z;
			return bounds;
		}

		/// <summary>
		/// Flips the coordinates for 2D mode
		/// </summary>
		/// <returns>The Y.</returns>
		/// <param name="bounds">Bounds.</param>
		public static IntVector FlipYZ(IntVector bounds) {
			var z = bounds.z;
			bounds.z = bounds.y;
			bounds.y = z;
			return bounds;
		}

        public static void Abs(ref Vector3 v)
        {
            v.x = Mathf.Abs(v.x);
            v.y = Mathf.Abs(v.y);
            v.z = Mathf.Abs(v.z);
        }

		/// <summary>
		/// Flag to indicate an invalid location
		/// </summary>
		public static readonly int INVALID_LOCATION = -1000000;
    }
}
