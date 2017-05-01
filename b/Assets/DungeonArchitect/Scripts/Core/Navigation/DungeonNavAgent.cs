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
using DungeonArchitect.Utils;
using SharpNav;
using SharpNav.Crowds;

namespace DungeonArchitect.Navigation {
	public abstract class DungeonNavAgent : MonoBehaviour {
		public abstract void Resume();
		public abstract void Stop();
		public abstract float GetRemainingDistance();
		public abstract Vector3 Destination { get; set; }
		public abstract Vector3 Velocity { get; set; }
		public abstract Vector3 Direction { get; }
	}
}
