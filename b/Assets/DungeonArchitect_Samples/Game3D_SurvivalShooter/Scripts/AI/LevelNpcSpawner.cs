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
using DungeonArchitect;

namespace DAShooter
{
	public class LevelNpcSpawner : DungeonEventListener {
		public GameObject parentObject;
		public GameObject[] npcTemplates;
		public Vector3 npcOffset = Vector3.zero;
	    public float spawnProbability = 0.25f;

		public override void OnPostDungeonBuild (Dungeon dungeon, DungeonModel model)
		{
			RebuildNPCs();
		}

		public void RebuildNPCs() {
			DestroyOldNpcs();
			if (npcTemplates.Length == 0) return;

			var waypoints = GameObject.FindObjectsOfType<Waypoint>();

			// Spawn an npc in each waypoint
			foreach (var waypoint in waypoints) {
	            if (Random.value < spawnProbability)
	            {
	                var position = waypoint.transform.position + npcOffset;
	                position = GetValidPointOnNavMesh(position);
	                var npcIndex = Random.Range(0, npcTemplates.Length);
	                var template = npcTemplates[npcIndex];
	                var npc = Instantiate(template, position, Quaternion.identity) as GameObject;

	                if (parentObject != null)
	                {
	                    npc.transform.parent = parentObject.transform;
	                }
	            }
			}
		}

		Vector3 GetValidPointOnNavMesh(Vector3 position) {
			UnityEngine.AI.NavMeshHit hit;
			if (UnityEngine.AI.NavMesh.SamplePosition(position, out hit, 4.0f, UnityEngine.AI.NavMesh.AllAreas)) {
				return hit.position;
			}
			return position;
		}

		public override void OnDungeonDestroyed(Dungeon dungeon) {
			DestroyOldNpcs();
		}

		void DestroyOldNpcs() {
			if (parentObject == null) {
				return;
			}

			var npcs = new List<GameObject>();
			var parentTransform = parentObject.transform;
			for(int i = 0; i < parentTransform.childCount; i++) {
				var npc = parentObject.transform.GetChild(i).gameObject;
				npcs.Add(npc);
			}

			foreach (var npc in npcs) {
				if (Application.isPlaying) {
					Destroy(npc);
				} else {
					DestroyImmediate(npc);
				}
			}
		}
	}
}
