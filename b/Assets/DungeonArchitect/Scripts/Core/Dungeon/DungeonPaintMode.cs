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
using DungeonArchitect.Utils;

namespace DungeonArchitect
{
    /// <summary>
    /// Manage the editor paint mode so you can paint the layout of you dungeon.
    /// You should implement your own paint mode depending on your dungeon builder's 
    /// data structures and requirements
    /// </summary>
	public abstract class DungeonPaintMode : MonoBehaviour {
		protected Dungeon dungeon;
        protected DungeonModel dungeonModel;
        protected DungeonConfig dungeonConfig;

        /// <summary>
        /// Gets the configuration of the dungeon
        /// </summary>
        /// <returns></returns>
		public DungeonConfig GetDungeonConfig() {
			if (dungeonConfig == null) {
				dungeonConfig = GetSiblingComponent<DungeonConfig>();
			}
			return dungeonConfig;
		}

        /// <summary>
        /// Gets the model used by the owning dungeon
        /// </summary>
        /// <returns></returns>
        public DungeonModel GetDungeonModel()
        {
			if (dungeonModel == null) {
				dungeonModel = GetSiblingComponent<DungeonModel>();
			}
			return dungeonModel;
		}

        /// <summary>
        /// Gets the owning dungeon
        /// </summary>
        /// <returns>The owning dungeon</returns>
        public Dungeon GetDungeon()
        {
			if (dungeon == null) {
				dungeon = GetSiblingComponent<Dungeon>();
			}
			return dungeon;
		}

		public T GetSiblingComponent<T>() {
			var parentTransform = gameObject.transform.parent;
			if (parentTransform != null && parentTransform.gameObject != null) {
				var dungeonGO = parentTransform.gameObject;
				return dungeonGO.GetComponent<T>();
			}
			return default(T);
		}
	}
}
