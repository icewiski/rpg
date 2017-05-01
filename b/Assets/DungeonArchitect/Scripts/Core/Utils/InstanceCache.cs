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

namespace DungeonArchitect.Utils
{
    /// <summary>
    /// Caches instances by their name so they can be reused when needed again instead of recreating it
    /// </summary>
    public class InstanceCache
    {
        Dictionary<string, object> InstanceByType = new Dictionary<string, object>();
        /// <summary>
        /// Retrieves the instance of the specified ScriptableObject type name. If none exists, a new one is created and stored
        /// </summary>
        /// <param name="typeName">The typename of the ScriptableObject</param>
        /// <returns>The cached instance of the specified ScriptableObject typename</returns>
        public object GetInstance(string typeName)
        {
            if (!InstanceByType.ContainsKey(typeName))
            {
                var type = System.Type.GetType(typeName);
                var obj = ScriptableObject.CreateInstance(type);
                InstanceByType.Add(typeName, obj);
            }
            return InstanceByType[typeName];
        }
    }
}
