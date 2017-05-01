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
using System.Collections.Generic;

using DungeonArchitect.Graphs;
namespace DungeonArchitect.Editors
{
    /// <summary>
    /// Custom property editor for graph objects
    /// Shows the graph editor when a theme graph asset is selected
    /// </summary>
    [CustomEditor(typeof(Graph))]
    public class GraphInspector : Editor
    {
        SerializedObject sobject;

        public void OnEnable()
        {
            sobject = new SerializedObject(target);
        }

        public override void OnInspectorGUI()
        {
            sobject.Update();
            GUILayout.Label("Dungeon Theme", EditorStyles.boldLabel);

            sobject.ApplyModifiedProperties();

            ///ShowEditor();
        }

        void ShowEditor()
        {
            var graph = target as Graph;
            if (graph != null)
            {
                var window = EditorWindow.GetWindow<DungeonArchitectGraphEditor>();
                if (window != null)
                {
                    window.Init(graph);
                }
            }
            else
            {
                Debug.LogWarning("Invalid Dungeon theme file");
            }
        }
    }
}
