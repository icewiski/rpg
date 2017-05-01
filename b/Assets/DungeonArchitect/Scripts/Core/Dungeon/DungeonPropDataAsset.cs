/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

//$ Copyright 2016, Code Respawn Technologies Pvt Ltd - All Rights Reserved $//

using System;
using UnityEngine;
using System.Collections.Generic;
using DungeonArchitect.Graphs;

namespace DungeonArchitect {
    /// <summary>
    /// The data-structure for serializing the theme graph to disk
    /// </summary>
	public class DungeonPropDataAsset {
		public List<PropTypeData> Props = new List<PropTypeData>();

		public void BuildFromGraph(Graph graph) {
			Props.Clear();
			if (graph == null) {
				return;
			}
			var nodes = graph.Nodes.ToArray();
			Array.Sort (nodes, new LeftToRightNodeComparer ());

			foreach (var node in nodes) {
				if (node is VisualNode) {
					var visualNode = node as VisualNode;

					foreach (var meshParentNode in visualNode.GetParentNodes()) {
						if (meshParentNode is MarkerNode) {
							var markerNode = meshParentNode as MarkerNode;
							
							PropTypeData item = null;
							if (visualNode is GameObjectNode) {
								var meshItem = new GameObjectPropTypeData();
								var goNode = visualNode as GameObjectNode;

								meshItem.Template = goNode.Template;

								item = meshItem;
							}
							else if (visualNode is SpriteNode) {
								var spriteItem = new SpritePropTypeData();
								var spriteNode = visualNode as SpriteNode;

								spriteItem.sprite = spriteNode.sprite;
								spriteItem.color = spriteNode.color;
								spriteItem.materialOverride = spriteNode.materialOverride;
								spriteItem.sortingLayerName = spriteNode.sortingLayerName;
								spriteItem.orderInLayer = spriteNode.orderInLayer;

								spriteItem.collisionType = spriteNode.collisionType;
								spriteItem.physicsMaterial = spriteNode.physicsMaterial;
								spriteItem.physicsOffset = spriteNode.physicsOffset;
								spriteItem.physicsSize = spriteNode.physicsSize;
								spriteItem.physicsRadius = spriteNode.physicsRadius;

								item = spriteItem;
							}
							else {
								// Unsupported visual node type
								continue;
							}

							// Set the common settings
							item.NodeId = visualNode.Id.ToString();
							item.AttachToSocket = markerNode.Caption;
							item.Affinity = visualNode.attachmentProbability;
							item.ConsumeOnAttach = visualNode.consumeOnAttach;
							item.Offset = visualNode.offset;
							item.IsStaticObject = visualNode.IsStatic;
							item.affectsNavigation = visualNode.affectsNavigation;
							item.UseSelectionRule = visualNode.selectionRuleEnabled;
							item.SelectorRuleClassName = visualNode.selectionRuleClassName;
							item.UseTransformRule = visualNode.transformRuleEnabled;
							item.TransformRuleClassName = visualNode.transformRuleClassName;
                            item.useSpatialConstraint = visualNode.useSpatialConstraint;
                            item.spatialConstraint = visualNode.spatialConstraint;

							var emitterNodes = visualNode.GetChildNodes();
							foreach (var childNode in emitterNodes) {
								if (childNode is MarkerEmitterNode) {
									var emitterNode = childNode as MarkerEmitterNode;
									if (emitterNode.Marker != null) {
										PropChildSocketData childData = new PropChildSocketData();
										childData.Offset = emitterNode.offset;
										childData.SocketType = emitterNode.Marker.Caption;
										item.ChildSockets.Add (childData);
									}
								}
							}
							Props.Add(item);
						}
					}
				}
			}
		}
	}
	
	/// <summary>
	/// Sorts the nodes from left to right based on the X-axis.
    /// This is used for sorting the visual nodes for execution, 
    /// since they are executed from left to right
	/// </summary>
	public class LeftToRightNodeComparer : IComparer<GraphNode>
	{
		public int Compare(GraphNode a, GraphNode b)  
		{
			if (a.Bounds.x == b.Bounds.x) {
				return 0;
			}
			return (a.Bounds.x < b.Bounds.x) ? -1 : 1;
		}
	}
}