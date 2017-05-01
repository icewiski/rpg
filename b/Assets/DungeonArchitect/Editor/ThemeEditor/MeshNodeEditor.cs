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
using DungeonArchitect.Graphs;

namespace DungeonArchitect.Editors
{
    /// <summary>
    /// Custom property editors for GameObjectNode
    /// </summary>
    [CanEditMultipleObjects]
    [CustomEditor(typeof(GameObjectNode))]
    public class MeshNodeEditor : VisualNodeEditor
    {
        SerializedProperty Template;

        public override void OnEnable()
        {
            base.OnEnable();
            drawOffset = true;
            drawAttachments = true;
            Template = sobject.FindProperty("Template");
        }

        protected override void DrawPreInspectorGUI()
        {
            GUILayout.Label("Game Object Node", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(Template);

            base.DrawPreInspectorGUI();
        }
    }

    /// <summary>
    /// Renders a mesh node
    /// </summary>
    public class MeshNodeRenderer : VisualNodeRenderer
    {
        protected override Object GetThumbObject(GraphNode node)
        {
            var meshNode = node as GameObjectNode;
            if (meshNode == null) return null;
            return meshNode.Template;
        }
    }

}
