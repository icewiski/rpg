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
using System;
using System.Collections;
using DungeonArchitect;
using DungeonArchitect.Constraints;
using DungeonArchitect.Constraints.Grid;

namespace DungeonArchitect.Editors
{
    public abstract class SpatialConstraintEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            DrawConstraintEditor(target as SpatialConstraint);
        }

        public virtual void DrawConstraintEditor(SpatialConstraint constraint)
        {
            GUILayout.Label("Editor not implemented");
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class ConstraintEditorAttribute : System.Attribute
    {
        public Type constraintType;

        public ConstraintEditorAttribute(Type constraintType)
        {
            this.constraintType = constraintType;
        }
    }
}
