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
using DungeonArchitect.Constraints;

namespace DungeonArchitect.Graphs
{
	
	[System.Serializable]
	public class VisualNode : PlaceableNode {
		/// <summary>
		/// Indicates if the game object created from this visual node is set to static
		/// If you are spawning NPCs or other dynamic objects, uncheck this
		/// </summary>
		public bool IsStatic = true;

		/// <summary>
		/// Indicates of the geometry in this node contributes to the navigation mesh
		/// You should enable this only if necessary to improve navmesh generation performance
		/// </summary>
		public bool affectsNavigation = false;

		/// <summary>
		/// Indicates if the selection rule is enabled.  The selection rule will not run if this is disabled
		/// </summary>
		public bool selectionRuleEnabled = false;

		/// <summary>
		/// The class name of the selection rule. 
		/// Selection rules let you specify behavior logic for selecting your nodes
		/// </summary>
		public string selectionRuleClassName;

		/// <summary>
		/// Indicates if the transform rule is enabled.  The transform rule will not run if this is disabled
		/// </summary>
		public bool transformRuleEnabled = false;

		/// <summary>
		/// The class name of the transformation rule.  
		/// Transform rules let you specify behavior logic to apply the offset on the nodes
		/// </summary>
		public string transformRuleClassName;

        /// <summary>
        /// Flag to indicate if spatial constraints are to be used
        /// </summary>
        public bool useSpatialConstraint = false;

        /// <summary>
        /// Spatial constraints lets you select a node based on nearby neighbor constraints
        /// </summary>
        [SerializeField]
        public SpatialConstraint spatialConstraint;
        
		public override void Initialize(string id, Graph graph) {
			base.Initialize(id, graph);
			Size = new Vector2(120, 120);
			bool createInputPins = false;
			bool createOutputPins = false;
			
			if (inputPins == null) {
				inputPins = new List<GraphPin>();
				createInputPins = true;
			}
			if (outputPins == null) {
				outputPins = new List<GraphPin>();
				createOutputPins = true;
			}
			
			if (createInputPins) {
				// Create an input pin on the top
				CreatePin(GraphPinType.Input,
				          new Vector2(60, 0),
				          new Rect(-40, 0, 80, 15),
				          new Vector2(0, -1));
			}
			
			if (createOutputPins) {
				// Create an output pin at the bottom
				CreatePin(GraphPinType.Output,
				          new Vector2(60, 120),
				          new Rect(-40, -15, 80, 15),
				          new Vector2(0, 1));
			}

		}
		
		public override void CopyFrom(GraphNode node)
		{
			base.CopyFrom(node);
			
			var visualNode = node as VisualNode;
			if (visualNode == null) return;
			
			IsStatic = visualNode.IsStatic;
			affectsNavigation = visualNode.affectsNavigation;
			selectionRuleEnabled = visualNode.selectionRuleEnabled;
			selectionRuleClassName = visualNode.selectionRuleClassName;
			transformRuleEnabled = visualNode.transformRuleEnabled;
			transformRuleClassName = visualNode.transformRuleClassName;
		}

	}

}
