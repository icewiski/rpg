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
using System.Collections.Generic;

namespace DungeonArchitect.Utils
{
    /// <summary>
    /// A blackboard holds global data that can be shared across multiple scripts
    /// </summary>
    public class Blackboard
    {
        private BlackboardDatabase<int> intEntries = new BlackboardDatabase<int>(0);
        public BlackboardDatabase<int> IntEntries
        {
            get { return intEntries; }
        }
        private BlackboardDatabase<float> floatEntries = new BlackboardDatabase<float>(0.0f);
        public BlackboardDatabase<float> FloatEntries
        {
            get { return floatEntries; }
        }
        private BlackboardDatabase<string> stringEntries = new BlackboardDatabase<string>("");
        public BlackboardDatabase<string> StringEntries
        {
            get { return stringEntries; }
        }
        private BlackboardDatabase<Vector3> vectorEntries = new BlackboardDatabase<Vector3>(Vector3.zero);
        public BlackboardDatabase<Vector3> VectorEntries
        {
            get { return vectorEntries; }
        }
        private BlackboardDatabase<IntVector> intVectorEntries = new BlackboardDatabase<IntVector>(IntVector.Zero);
        public BlackboardDatabase<IntVector> IntVectorEntries
        {
            get { return intVectorEntries; }
        }
    }

    
    public class BlackboardDatabase<T>
    {
        T defaultValue;
        Dictionary<string, T> database = new Dictionary<string, T>();

        public BlackboardDatabase(T defaultValue)
        {
            this.defaultValue = defaultValue;
        }

        public void SetValue(string key, T value) {
            database[key] = value;
        }

        public T GetValue(string key)
        {
            if (!database.ContainsKey(key))
            {
                return defaultValue;
            }
            return database[key];
        }
        
    }

}
