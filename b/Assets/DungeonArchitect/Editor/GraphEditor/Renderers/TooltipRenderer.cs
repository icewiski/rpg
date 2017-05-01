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

namespace DungeonArchitect.Editors
{
    /// <summary>
    /// Graph tooltip singleton
    /// </summary>
    public class GraphTooltip
    {
        /// <summary>
        /// Set this to display a tooltip in the graph editor
        /// </summary>
        public static string message = "";
        public static void Clear()
        {
            message = "";
        }
    }

    /// <summary>
    /// Renders a tooltip in the graph editor. The tooltip message is defined in GraphTooltip.message
    /// </summary>
    public class GraphTooltipRenderer
    {
        public static void Draw(GraphRendererContext rendererContext, Vector2 mousePosition)
        {
            if (GraphTooltip.message == null || GraphTooltip.message.Trim().Length == 0) return;

            var tooltipPadding = new Vector2(4, 4);

            var drawPosition = mousePosition + new Vector2(15, 5);

            var tooltipString = GraphTooltip.message;
            var style = GUI.skin.GetStyle("label");
            var contentSize = style.CalcSize(new GUIContent(tooltipString));
            drawPosition -= tooltipPadding;
            contentSize += tooltipPadding * 2;
            var bounds = new Rect(drawPosition.x, drawPosition.y, contentSize.x, contentSize.y);

            GUI.backgroundColor = new Color(.15f, .15f, .15f);
            GUI.Box(bounds, "");

            var innerGlow = rendererContext.Resources.GetResource<Texture2D>(DungeonEditorResources.TEXTURE_PIN_GLOW);
            GUI.DrawTexture(bounds, innerGlow);

            style.alignment = TextAnchor.MiddleCenter;
            GUI.color = Color.white;
            GUI.Label(bounds, tooltipString, style);

        }
    }
}
