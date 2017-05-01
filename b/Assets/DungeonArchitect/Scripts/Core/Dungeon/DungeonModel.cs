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
using System;
using System.Collections.Generic;
using DungeonArchitect.Utils;

namespace DungeonArchitect
{
    /// <summary>
    /// Abstract dungeon model.  Create your own implementation of the model depending on your builder's needs
    /// </summary>
	//[System.Serializable]
	public abstract class DungeonModel : MonoBehaviour
	{
        void Reset()
        {
            ResetModel();
        }

        public virtual void ResetModel() { }

        [SerializeField]
        //[HideInInspector]
        public DungeonToolData ToolData;

        public virtual DungeonToolData CreateToolDataInstance()
        {
            return ScriptableObject.CreateInstance<DungeonToolData>();
        }

	}


    /// <summary>
    /// Tool Data represented by the grid based builder
    /// </summary>
    [Serializable]
    public class DungeonToolData : ScriptableObject
    {
        // The cells painted by the "Paint" tool
        [SerializeField]
        List<IntVector> paintedCells = new List<IntVector>();
        public List<IntVector> PaintedCells
        {
            get
            {
                return paintedCells;
            }
        }
    }
}
