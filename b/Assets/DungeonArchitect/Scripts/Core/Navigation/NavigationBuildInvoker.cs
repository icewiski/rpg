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
using DungeonArchitect;
using DungeonArchitect.Navigation;

namespace DungeonArchitect.Navigation {
	/// <summary>
	/// Drop this script into your dungeon object and assign the nav mesh prefab to
	/// automatically rebuild the nav mesh whenever the dungeon is rebuild (works both with runtime and design time)
	/// </summary>
	public class NavigationBuildInvoker : DungeonEventListener {
		public DungeonNavMesh navMesh;

		/// <summary>
		/// Called after the dungeon is completely built
		/// </summary>
		/// <param name="model">The dungeon model</param>
		public override void OnPostDungeonBuild(Dungeon dungeon, DungeonModel model) {
			if (navMesh != null) {
				navMesh.Build();
			}
			else {
				Debug.LogWarning("Cannot automatically rebuild nav mesh as it is not assigned to the dungeon event listener");
			}
		}

	}
}