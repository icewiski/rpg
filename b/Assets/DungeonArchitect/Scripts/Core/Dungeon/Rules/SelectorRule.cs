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
    /// Selector rule allow you to attach selection behavior to decide if a visual node should be inserted into the scene
    /// </summary>
    public class SelectorRule : ScriptableObject
    {
        /// <summary>
        /// Implementations should override this and determine if the node should be selected (inserted into the scene)
        /// </summary>
        /// <param name="socket">The marker data-structure</param>
        /// <param name="propTransform">The combined transform of the visual node that invoked this rule</param>
        /// <param name="model">The dungeon model</param>
        /// <param name="random">The random stream used by the builder. User this random stream for any randomness for consistancy</param>
        /// <returns></returns>
        public virtual bool CanSelect(PropSocket socket, Matrix4x4 propTransform, DungeonModel model, System.Random random)
        {
            return true;
        }
    }
}
