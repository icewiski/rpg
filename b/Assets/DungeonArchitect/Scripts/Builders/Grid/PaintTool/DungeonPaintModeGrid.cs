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

namespace DungeonArchitect.Builders.Grid
{
    /// <summary>
    /// Editor tooling for the grid based dungeon builder. Lets you paint with a grid based brush
    /// </summary>
    [ExecuteInEditMode]
    public class DungeonPaintModeGrid : DungeonPaintMode
    {
        /// <summary>
        /// The height of the cursor in grid cooridnates. Can also be changed with the mouse wheel in the editor when activated
        /// </summary>
        public int cursorLogicalHeight = 0;

        /// <summary>
        /// The opacity of the overlay colored tiles
        /// </summary>
        public float overlayOpacity = 0.1f;

		/// <summary>
		/// Indicates if the painting is to be done in 2D mode (for 2D dungeons)
		/// This flag is used for the editor tooling.  The model still stores it in 3D
		/// </summary>
		public bool mode2D = false;

		/// <summary>
		/// The size of the brush.  This would create a brush of size NxN
		/// </summary>
		public int brushSize = 1;

        /// <summary>
        /// Reference to the grid model used by the grid builder
        /// </summary>
        private GridDungeonModel gridModel;

        public float GetCursorHeight()
        {
            var gridConfig = GetDungeonConfig() as GridDungeonConfig;
            if (gridConfig == null) return 0;
            return cursorLogicalHeight * gridConfig.GridCellSize.y;
        }

        public void SetElevationDelta(int delta)
        {
            cursorLogicalHeight += delta;
        }

        
        public GridDungeonModel GetDungeonModelGrid()
        {
            var model = base.GetDungeonModel();
            gridModel = model as GridDungeonModel;
            if (gridModel == null)
            {
                Debug.LogWarning("Invalid dungeon model type for this type of paint tool.  Expected DungeonModelGrid.  Received:" + (model != null ? model.GetType().ToString() : "null"));
            }

            return gridModel;
        }
    }
}
