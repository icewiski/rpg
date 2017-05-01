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


namespace DungeonArchitect.Terrains
{
    /// <summary>
    /// The type of the texture defined in the landscape paint settings.  
    /// This determines how the specified texture would be painted in the modified terrain
    /// </summary>
    public enum LandscapeTextureType
    {
        Fill,
        Room,
        Corridor,
        Cliff
    }

    /// <summary>
    /// Data-structure to hold the texture settings.  This contains enough information to paint the texture 
    /// on to the terrain
    /// </summary>
    [System.Serializable]
    public class LandscapeTexture
    {
        public LandscapeTextureType textureType;
        public Texture2D diffuse;
        public Texture2D normal;
        public float metallic = 0;
        public Vector2 size = new Vector2(15, 15);
        public Vector2 offset = Vector2.zero;
    }
}
