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
using System.Collections;


namespace DAShooter
{
	public class WaypointPathVisualizer : MonoBehaviour {
		public Color pathColor = Color.cyan;

		void OnDrawGizmosSelected() {
			DrawWaypointPaths();
		}

		void DrawWaypointPaths() {
			Gizmos.color = pathColor;
			// Draw the connection of waypoints
			var waypoints = GameObject.FindObjectsOfType<Waypoint>();
			foreach (var waypoint in waypoints) {
				if (waypoint == null) continue;
				var startPosition = waypoint.gameObject.transform.position;
				DrawPoint(startPosition);
				foreach (var adjacentWaypoint in waypoint.AdjacentWaypoints) {
					var endPosition = adjacentWaypoint.gameObject.transform.position;
					DrawLine(startPosition, endPosition);
				}
			}
		}

		void DrawLine(Vector3 a, Vector3 b) {
			/*
			if (mode2D) {
				Gizmos.DrawLine(FlipYZ(a), FlipYZ(b));
			}
			else {
				Gizmos.DrawLine(a, b);
			}
			*/
			Gizmos.DrawLine(a, b);
		}

		void DrawPoint(Vector3 p) {
			/*
			if (mode2D) {
				Gizmos.DrawWireSphere(FlipYZ(p), 0.1f);
			} else {
				Gizmos.DrawWireSphere(p, 0.1f);
			}
			*/
			Gizmos.DrawWireSphere(p, 0.1f);
		}

		Vector3 FlipYZ(Vector3 v) {
			return new Vector3(v.x, v.z, v.y);
		}
	}
}
