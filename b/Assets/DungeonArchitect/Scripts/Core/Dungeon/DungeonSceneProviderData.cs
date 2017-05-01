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

namespace DungeonArchitect
{
    /// <summary>
    /// Meta-data added to each spawned game object in the scene.  This is used to identify objects that belong to the dungeons, for later destruction and rebuilding
    /// </summary>
    public class DungeonSceneProviderData : MonoBehaviour
    {
		/// <summary>
		/// The graph node id this game object was spawned from in the theme graph
		/// </summary>
        public string NodeId;

		/// <summary>
		/// The dungeon this game object belongs to
		/// </summary>
        public Dungeon dungeon;

		/// <summary>
		/// Indicates if the geometry in this node contributes to navigation mesh generation
		/// This flag reflects the state set in the theme graph's visual node affectsNavigation flag
		/// </summary>
		public bool affectsNavigation = false;
    }
}
