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
using DungeonArchitect.Graphs;

namespace DungeonArchitect.Editors
{
    /// <summary>
    /// Custom property editors for MarkerNode
    /// </summary>
    [CustomEditor(typeof(MarkerNode))]
    public class MarkerNodeEditor : Editor
    {
        SerializedObject sobject;
        SerializedProperty caption;

        public void OnEnable()
        {
            sobject = new SerializedObject(target);
            caption = sobject.FindProperty("caption");
        }

        public override void OnInspectorGUI()
        {
            sobject.Update();
            GUILayout.Label("Marker Node", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(caption, new GUIContent("Name"));
            sobject.ApplyModifiedProperties();
        }
    }


    /// <summary>
    /// Renders a marker node
    /// </summary>
    public class MarkerNodeRenderer : GraphNodeRenderer
    {
        public override void Draw(GraphRendererContext rendererContext, GraphNode node, GraphCamera camera)
        {
            // Draw the background base texture
            DrawNodeTexture(rendererContext, node, camera, DungeonEditorResources.TEXTURE_MARKER_NODE_BG);

            var style = GUI.skin.GetStyle("Label");
            style.alignment = TextAnchor.MiddleCenter;

            var positionScreen = camera.WorldToScreen(node.Position);
            var pinHeight = node.OutputPins[0].BoundsOffset.height;
            var labelBounds = new Rect(positionScreen.x, positionScreen.y, node.Bounds.width, node.Bounds.height - pinHeight / 2);
            style.normal.textColor = node.Selected ? GraphEditorConstants.TEXT_COLOR_SELECTED : GraphEditorConstants.TEXT_COLOR;
            GUI.Label(labelBounds, node.Caption, style);

            // Draw the foreground frame textures
            DrawNodeTexture(rendererContext, node, camera, DungeonEditorResources.TEXTURE_MARKER_NODE_FRAME);

            if (node.Selected)
            {
                DrawNodeTexture(rendererContext, node, camera, DungeonEditorResources.TEXTURE_MARKER_NODE_SELECTION);
            }

            // Draw the pins
            base.Draw(rendererContext, node, camera);

        }
    }
}
