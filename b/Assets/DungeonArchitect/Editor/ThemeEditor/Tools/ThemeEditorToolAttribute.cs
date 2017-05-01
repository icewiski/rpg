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
using System.Linq;
using System.Collections.Generic;
using DungeonArchitect.Editors;

namespace DungeonArchitect.Editors
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ThemeEditorToolAttribute : Attribute
    {
        string path;
        public string Path
        {
            get { return path; }
        }

        int priority;
        public int Priority
        {
            get { return priority; }
        }

        public ThemeEditorToolAttribute(string path, int priority)
        {
            this.path = path;
            this.priority = priority;
        }
    }

    public delegate void ThemeEditorToolFunctionDelegate(DungeonArchitectGraphEditor editor);

    public class ThemeEditorToolFunctionInfo
    {
        public ThemeEditorToolFunctionDelegate function;
        public ThemeEditorToolAttribute attribute;
    }
}
