/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

//$ Copyright 2016, Code Respawn Technologies Pvt Ltd - All Rights Reserved $//

using UnityEngine;
using System.Collections.Generic;

namespace DungeonArchitect
{
    /// <summary>
    /// Represent an integer vector
    /// </summary>
	[System.Serializable]
	public struct IntVector {
		[SerializeField]
		public int x;

		[SerializeField]
		public int y;

		[SerializeField]
		public int z;

		public IntVector(int x, int y, int z) {
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public void Set(int x, int y, int z) {
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public float DistanceSq() {
			return x * x + y * y + z * z;
		}

		public float Distance() {
			return Mathf.Sqrt(x * x + y * y + z * z);
		}

		public static IntVector operator+(IntVector a, IntVector b) {
			var result = new IntVector();
			result.x = a.x + b.x;
			result.y = a.y + b.y;
			result.z = a.z + b.z;
			return result;
		}
		public static IntVector operator-(IntVector a, IntVector b) {
			var result = new IntVector();
			result.x = a.x - b.x;
			result.y = a.y - b.y;
			result.z = a.z - b.z;
			return result;
		}
		public static IntVector operator*(IntVector a, IntVector b) {
			var result = new IntVector();
			result.x = a.x * b.x;
			result.y = a.y * b.y;
			result.z = a.z * b.z;
			return result;
		}
        public static Vector3 operator *(IntVector a, Vector3 b)
        {
            var result = new Vector3();
            result.x = a.x * b.x;
            result.y = a.y * b.y;
            result.z = a.z * b.z;
            return result;
        }
		public static IntVector operator/(IntVector a, IntVector b) {
			var result = new IntVector();
			result.x = a.x / b.x;
			result.y = a.y / b.y;
			result.z = a.z / b.z;
			return result;
		}
		
		public static IntVector operator+(IntVector a, int b) {
			var result = new IntVector();
			result.x = a.x + b;
			result.y = a.y + b;
			result.z = a.z + b;
			return result;
		}
		public static IntVector operator-(IntVector a, int b) {
			var result = new IntVector();
			result.x = a.x - b;
			result.y = a.y - b;
			result.z = a.z - b;
			return result;
		}
		public static IntVector operator*(IntVector a, int b) {
			var result = new IntVector();
			result.x = a.x * b;
			result.y = a.y * b;
			result.z = a.z * b;
			return result;
		}
		public static IntVector operator/(IntVector a, int b) {
			var result = new IntVector();
			result.x = a.x / b;
			result.y = a.y / b;
			result.z = a.z / b;
			return result;
		}

		public override bool Equals(System.Object obj)
		{
			if (obj is IntVector) {
				var other = (IntVector)obj;
				return this.x == other.x &&
					this.y == other.y &&
					this.z == other.z;
			}
			return false;
		}
		public override int GetHashCode()
		{
			return (x ^ (y << 16)) ^ (z << 24);
		}

		public static Vector3 ToV3(IntVector iv) {
			return new Vector3(iv.x, iv.y, iv.z);
		}

		public static readonly IntVector Zero = new IntVector(0, 0, 0);
	}
}
