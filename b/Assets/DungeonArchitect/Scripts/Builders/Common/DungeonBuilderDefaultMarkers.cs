/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System;
using System.Collections.Generic;
using DungeonArchitect;
using DungeonArchitect.Utils;
using DungeonArchitect.Builders.Grid;
using DungeonArchitect.Builders.FloorPlan;
using DungeonArchitect.Builders.Isaac;
using DungeonArchitect.Builders.SimpleCity;
using DungeonArchitect.Builders.Snap;

namespace DungeonArchitect.Builders
{
    public class DungeonBuilderDefaultMarkers
    {
        Dictionary<Type, string[]> DefaultMarkersByBuilder = new Dictionary<Type, string[]>();

        public DungeonBuilderDefaultMarkers()
        {
            DefaultMarkersByBuilder.Add(typeof(GridDungeonBuilder), new string[] {
            DungeonConstants.ST_GROUND,
            DungeonConstants.ST_WALL,
            DungeonConstants.ST_WALLSEPARATOR,
            DungeonConstants.ST_FENCE,
            DungeonConstants.ST_FENCESEPARATOR,
            DungeonConstants.ST_DOOR,
            DungeonConstants.ST_STAIR,
            DungeonConstants.ST_STAIR2X,
            DungeonConstants.ST_WALLHALF,
            DungeonConstants.ST_WALLHALFSEPARATOR
        });

            DefaultMarkersByBuilder.Add(typeof(SimpleCityDungeonBuilder), new string[] {
            SimpleCityDungeonConstants.House,
            SimpleCityDungeonConstants.House2X,
            SimpleCityDungeonConstants.Park,
            SimpleCityDungeonConstants.Road_X,
            SimpleCityDungeonConstants.Road_T,
            SimpleCityDungeonConstants.Road_Corner,
            SimpleCityDungeonConstants.Road_S,
            SimpleCityDungeonConstants.Road,

            SimpleCityDungeonConstants.WallMarkerName,
            SimpleCityDungeonConstants.DoorMarkerName,
            SimpleCityDungeonConstants.GroundMarkerName,
            SimpleCityDungeonConstants.CornerTowerMarkerName,
            SimpleCityDungeonConstants.WallPaddingMarkerName,
        });

            DefaultMarkersByBuilder.Add(typeof(FloorPlanBuilder), new string[] {
            FloorPlanMarkers.MARKER_GROUND,
            FloorPlanMarkers.MARKER_CEILING,
            FloorPlanMarkers.MARKER_WALL,
            FloorPlanMarkers.MARKER_DOOR,
            FloorPlanMarkers.MARKER_BUILDING_WALL
        });

            DefaultMarkersByBuilder.Add(typeof(IsaacDungeonBuilder), new string[] {
            DungeonConstants.ST_GROUND,
            DungeonConstants.ST_WALL,
            DungeonConstants.ST_DOOR
        });

            DefaultMarkersByBuilder.Add(typeof(SnapBuilder), new string[] {

        });

        }

        public string[] GetDefaultMarkers(Type builderClass)
        {
            if (!DefaultMarkersByBuilder.ContainsKey(builderClass))
            {
                return new string[0];
            }

            return DefaultMarkersByBuilder[builderClass];
        }
    }
}
