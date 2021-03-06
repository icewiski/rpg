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
    /// Custom property editors for MarkerEmitterNode
    /// </summary>
    [CustomEditor(typeof(MarkerEmitterNode))]
    public class MarkerEmitterNodeEditor : PlaceableNodeEditor
    {

        public override void OnEnable()
        {
            base.OnEnable();
            drawOffset = true;
            drawAttachments = false;
        }

        protected override void DrawPreInspectorGUI()
        {
            var emitterNode = target as MarkerEmitterNode;
            var markerCaption = (emitterNode.Marker != null) ? emitterNode.Marker.Caption : "Unknown";
            GUILayout.Label("Marker Emitter Node: " + markerCaption, EditorStyles.boldLabel);
        }
    }

    /// <summary>
    /// Renders a MarkerEmitterNode
    /// </summary>
    public class MarkerEmitterNodeRenderer : GraphNodeRenderer
    {
        public override void Draw(GraphRendererContext rendererContext, GraphNode node, GraphCamera camera)
        {
            // Draw the background base texture
            DrawNodeTexture(rendererContext, node, camera, DungeonEditorResources.TEXTURE_MARKER_NODE_BG);

            var style = GUI.skin.GetStyle("Label");
            style.alignment = TextAnchor.MiddleCenter;

            var emitterNode = node as MarkerEmitterNode;
            var title = (emitterNode.Marker != null) ? emitterNode.Marker.Caption : "{NONE}";

            var positionScreen = camera.WorldToScreen(node.Position);
            var labelBounds = new Rect(positionScreen.x, positionScreen.y, node.Bounds.width, node.Bounds.height - 5);
            style.normal.textColor = node.Selected ? GraphEditorConstants.TEXT_COLOR_SELECTED : GraphEditorConstants.TEXT_COLOR;
            GUI.Label(labelBounds, title, style);

            // Draw the foreground frame textures
            DrawNodeTexture(rendererContext, node, camera, DungeonEditorResources.TEXTURE_MARKER_EMITTER_NODE_FRAME);

            if (node.Selected)
            {
                DrawNodeTexture(rendererContext, node, camera, DungeonEditorResources.TEXTURE_MARKER_NODE_SELECTION);
            }

            // Draw the pins
            base.Draw(rendererContext, node, camera);
        }

        protected override Color getBackgroundColor(GraphNode node)
        {
            var color = new Color(0.2f, 0.25f, 0.4f);
            return node.Selected ? GraphEditorConstants.NODE_COLOR_SELECTED : color;
        }

    }
}
