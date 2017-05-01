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
    /// Marker Emitters let you emit your own markers anywhere in the map.  Implement this class and add it to the Dungeon object
    /// to add your own markers right after the dungeon layout is created
    /// </summary>
    public class DungeonMarkerEmitter : MonoBehaviour {
        /// <summary>
        /// Called by the dungeon object right after the dungeon is created
        /// </summary>
        /// <param name="builder">reference to the builder object used to build the dungeon</param>
        public virtual void EmitMarkers(DungeonBuilder builder)
        {
        }
    }
}
