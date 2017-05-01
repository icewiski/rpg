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
using System.Collections;
using DungeonArchitect.Graphs;

namespace DungeonArchitect.Editors
{
    /// <summary>
    /// Renders a graph pin hosted inside a node
    /// </summary>
    public class GraphPinRenderer
    {

        public static void Draw(GraphRendererContext rendererContext, GraphPin pin, GraphCamera camera)
        {
            var bounds = new Rect(pin.GetBounds());
            var positionWorld = pin.Node.Position + bounds.position;
            var positionScreen = camera.WorldToScreen(positionWorld);
            bounds.position = positionScreen;

            var originalColor = GUI.backgroundColor;
            GUI.backgroundColor = GetPinColor(pin);
            GUI.Box(bounds, "");
            GUI.backgroundColor = originalColor;

            // Draw the pin glow

            var glowTexture = rendererContext.Resources.GetResource<Texture2D>(DungeonEditorResources.TEXTURE_PIN_GLOW);
            if (glowTexture != null)
            {
                GUI.DrawTexture(bounds, glowTexture);
            }
        }

        static Color GetPinColor(GraphPin pin)
        {
            Color color;
            if (pin.ClickState == GraphPinMouseState.Clicked)
            {
                color = GraphEditorConstants.PIN_COLOR_CLICK;
            }
            else if (pin.ClickState == GraphPinMouseState.Hover)
            {
                color = GraphEditorConstants.PIN_COLOR_HOVER;
            }
            else
            {
                color = GraphEditorConstants.PIN_COLOR;
            }
            return color;
        }

    }
}
