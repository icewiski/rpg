/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System.Collections;


namespace DungeonArchitect
{
    [System.Serializable]
    public class MarkerReplacementEntry
    {
        public string fromMarker;
        public string toMarker;
    }

    /// <summary>
    /// This volume replaces any specified markers found in the scene before theming is applied to it. 
    /// This helps in having more control over the generated dungeon, e.g. remove / add doors, walls etc
    /// </summary>
    [ExecuteInEditMode]
    public class MarkerReplaceVolume : Volume
    {
        void Awake()
        {
            COLOR_WIRE = new Color(1, 0.25f, 0.5f, 1);
            COLOR_SOLID_DESELECTED = new Color(1, 0.25f, 0.5f, 0.0f);
            COLOR_SOLID = new Color(1, 0.25f, 0.5f, 0.1f);
        }

        public MarkerReplacementEntry[] replacements;
    }
}
