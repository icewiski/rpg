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
using DungeonArchitect;
using System.Collections.Generic;

namespace DungeonArchitect
{

	/// <summary>
	/// Listen to various dungeon events during the build and destroy phase
	/// </summary>
	public class DungeonEventListener : MonoBehaviour {
		/// <summary>
        /// Called after the layout is built in memory, but before the markers are emitted
		/// </summary>
		/// <param name="model">The dungeon model</param>
		public virtual void OnPostDungeonLayoutBuild(Dungeon dungeon, DungeonModel model) {}

        /// <summary>
        /// Called after all the markers have been emitted for the level (but before the theming engine is run on those markers)
        /// This gives you an opportunity to modify the markers 
        /// </summary>
        /// <param name="dungeon"></param>
        /// <param name="model"></param>
        public virtual void OnDungeonMarkersEmitted(Dungeon dungeon, DungeonModel model, List<PropSocket> markers) { }

        /// <summary>
        /// Called after the dungeon is completely built
        /// </summary>
        /// <param name="model">The dungeon model</param>
		public virtual void OnPostDungeonBuild(Dungeon dungeon, DungeonModel model) {}

		/// <summary>
        /// Called after the dungeon is destroyed
        /// </summary>
        /// <param name="model">The dungeon model</param>
		public virtual void OnDungeonDestroyed(Dungeon dungeon) {}
	}
}
