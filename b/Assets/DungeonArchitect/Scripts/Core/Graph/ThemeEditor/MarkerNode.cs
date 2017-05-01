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
using System.Collections.Generic;

namespace DungeonArchitect.Graphs
{
	public class MarkerNode : GraphNode  {

		public override void Initialize(string id, Graph graph) {
			base.Initialize(id, graph);
			UpdateName("MarkerNode_");
			
			Size = new Vector2(120, 50);
			
			if (inputPins == null) {
				inputPins = new List<GraphPin>();
			}
			
			if (outputPins == null) {
				outputPins = new List<GraphPin>();

				CreatePin(GraphPinType.Output,
				          new Vector2(60, 48),
				          new Rect(-40, -15, 80, 15),
				          new Vector2(0, 1));
			}
			
			if (caption == null) {
				caption = "Marker";
			}
		}


	}
}
