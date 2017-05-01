/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

//$ Copyright 2016, Code Respawn Technologies Pvt Ltd - All Rights Reserved $//

using UnityEditor;
using UnityEngine;
using System.Collections;
using DungeonArchitect;
using DMathUtils = DungeonArchitect.Utils.MathUtils;

namespace DungeonArchitect.Editors
{
    /// <summary>
    /// Custom property editor for the paint mode object
    /// </summary>
    public class DungeonPaintModeEditor : Editor
    {
        void OnEnable()
        {
            SceneView.onSceneGUIDelegate += SceneGUI;
            var paintMode = target as DungeonPaintMode;
            if (paintMode != null)
            {
                var model = paintMode.GetDungeonModel();
                if (model.ToolData == null)
                {
                    Undo.RecordObjects(new Object[] {model}, "Enter Tool Mode");
                    model.ToolData = model.CreateToolDataInstance();
                    Undo.RecordObjects(new Object[] { model.ToolData }, "Create Tool Data");
                }
            }

        }

        void OnDisable()
        {
            SceneView.onSceneGUIDelegate -= SceneGUI;
        }

        protected virtual void SceneGUI(SceneView sceneview) { 
        }
    }
}
