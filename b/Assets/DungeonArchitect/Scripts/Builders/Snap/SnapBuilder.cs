/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System.Collections.Generic;
using DungeonArchitect;
using DungeonArchitect.Utils;

namespace DungeonArchitect.Builders.Snap
{
    public class SnapBuilder : DungeonBuilder
    {
        SnapConfig snapConfig;
        SnapModel snapModel;
        
        //new System.Random random;

        /// <summary>
        /// Builds the dungeon layout.  In this method, you should build your dungeon layout and save it in your model file
        /// No markers should be emitted here.   (EmitMarkers function will be called later by the engine to do that)
        /// </summary>
        /// <param name="config">The builder configuration</param>
        /// <param name="model">The dungeon model that the builder will populate</param>
        public override void BuildDungeon(DungeonConfig config, DungeonModel model)
        {
            base.BuildDungeon(config, model);

            //random = new System.Random((int)config.Seed);
            
            // We know that the dungeon prefab would have the appropriate config and models attached to it
            // Cast and save it for future reference
            snapConfig = config as SnapConfig;
            snapModel = model as SnapModel;
            snapModel.Config = snapConfig;

            propSockets.Clear();
        }

        /// <summary>
        /// Override the builder's emit marker function to emit our own markers based on the layout that we built
        /// You should emit your markers based on the layout you have saved in the model generated previously
        /// When the user is designing the theme interactively, this function will be called whenever the graph state changes,
        /// so the theme engine can populate the scene (BuildDungeon will not be called if there is no need to rebuild the layout again)
        /// </summary>
        public override void EmitMarkers()
        {
            base.EmitMarkers();

        }
        
        public override bool IsThemingSupported() { return false; }

        // This is called by the builders that do not support theming
        public override void BuildNonThemedDungeon() {
            Debug.Log("non themed dungeon executing");
        }
    }
}
