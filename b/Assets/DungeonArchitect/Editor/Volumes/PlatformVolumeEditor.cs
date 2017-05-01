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
using UnityEditor;
using System.Collections;
using DungeonArchitect;
using DungeonArchitect.Utils;
using DungeonArchitect.Builders.Grid;

namespace DungeonArchitect.Editors
{
    /// <summary>
    /// Custom property editor for Platform volumes
    /// </summary>
    [CustomEditor(typeof(PlatformVolume))]
    public class PlatformVolumeEditor : VolumeEditor
    {
        public override void OnUpdate(SceneView sceneView)
        {
            base.OnUpdate(sceneView);
            /*
            var platform = target as PlatformVolume;
            if (platform != null)
            {
                var transform = platform.gameObject.transform;
                if (transform.hasChanged)
                {
                    OnTransformModified(platform);
                    transform.hasChanged = false;
                }
            }
            */
        }

    }
}
