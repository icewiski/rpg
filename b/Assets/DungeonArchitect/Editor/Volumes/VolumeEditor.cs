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

namespace DungeonArchitect.Editors
{
    /// <summary>
    /// Custom property editor for volumes game objects
    /// </summary>
    [ExecuteInEditMode]
    public class VolumeEditor : Editor
    {
        IntVector positionOnGrid;
        IntVector sizeOnGrid;
        protected bool dynamicUpdate = true;
        protected bool onlyReapplyTheme = false;    // If true, Does not rebuild the layout and only applies the theme again over the existing layout

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Rebuild Dungeon"))
            {
                RequestRebuild(true);
            }
        }


        void OnEnable()
        {
            SceneView.onSceneGUIDelegate += OnUpdate;
        }

        void OnDisable()
        {
            SceneView.onSceneGUIDelegate -= OnUpdate;
        }

        public virtual void OnUpdate(SceneView sceneView)
        {
            if (dynamicUpdate)
            {
                var volume = target as Volume;
                if (volume != null)
                {
                    var transform = volume.gameObject.transform;
                    if (transform.hasChanged)
                    {
                        OnTransformModified(volume);
                        transform.hasChanged = false;
                    }
                }
            }
        }

        void RequestRebuild(bool force)
        {
            var volume = target as Volume;
            if (volume != null && volume.dungeon != null)
            {
                var dungeon = volume.dungeon;
                if (onlyReapplyTheme)
                {
                    dungeon.ReapplyTheme();
                }
                else
                {
                    if (force)
                    {
                        dungeon.Build();
                    }
                    else
                    {
                        dungeon.RequestRebuild();
                    }

                }
            }
        }

        protected void OnTransformModified(Volume volume)
        {
            if (volume == null || volume.dungeon == null)
            {
                return;
            }
            var builder = volume.dungeon.GetComponent<DungeonBuilder>();
            if (builder == null)
            {
                return;
            }

            IntVector newPositionOnGrid, newSizeOnGrid;
            builder.OnVolumePositionModified(volume, out newPositionOnGrid, out newSizeOnGrid);

            if (!positionOnGrid.Equals(newPositionOnGrid) || !sizeOnGrid.Equals(newSizeOnGrid))
            {
                positionOnGrid = newPositionOnGrid;
                sizeOnGrid = newSizeOnGrid;
                OnGridTransformModified();
            }

        }

        void OnGridTransformModified()
        {
            RequestRebuild(false);
        }
    }
}
