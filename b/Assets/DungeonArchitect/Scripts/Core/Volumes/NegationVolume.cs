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
    /// Negation volumes remove procedural geometries from the scene that lie with it's bounds
    /// </summary>
    [ExecuteInEditMode]
    public class NegationVolume : Volume
    {
        public bool inverse = false;

        void Awake()
        {
            COLOR_WIRE = new Color(1, 0.5f, 0, 1);
            COLOR_SOLID_DESELECTED = new Color(1, 0.5f, 0, 0.0f);
            COLOR_SOLID = new Color(1, 0.5f, 0, 0.1f);
        }
    }
}
