/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System.Collections.Generic;
using DungeonArchitect;
using DungeonArchitect.Utils;

public class MarkerTerrainClampListener : DungeonEventListener {
    
    public override void OnDungeonMarkersEmitted(Dungeon dungeon, DungeonModel model, List<PropSocket> markers) 
    {
        var terrain = Terrain.activeTerrain;
        if (terrain == null) return;

        foreach (var marker in markers)
        {
            var clampedPosition = GetClampedPosition(ref marker.Transform, terrain);
            Matrix.SetTranslation(ref marker.Transform, clampedPosition);
        }
    }

    Vector3 GetClampedPosition(ref Matrix4x4 mat, Terrain terrain)
    {
        var position = Matrix.GetTranslation(ref mat);
        position.y = LandscapeDataRasterizer.GetHeight(terrain, position.x, position.z);
        return position;
    }
}
