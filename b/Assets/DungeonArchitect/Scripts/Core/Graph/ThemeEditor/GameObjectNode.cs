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
	
	[System.Serializable]
	public class GameObjectNode : VisualNode {
		public GameObject Template;

		public override void Initialize(string id, Graph graph) {
			base.Initialize(id, graph);
			UpdateName("MeshNode_");

			if (caption == null) {
				caption = "Game Object Node";
			}
		}

        public override void CopyFrom(GraphNode node)
        {
            base.CopyFrom(node);

            var goNode = node as GameObjectNode;
            if (goNode == null) return;

            Template = goNode.Template;
        }
	}

}
