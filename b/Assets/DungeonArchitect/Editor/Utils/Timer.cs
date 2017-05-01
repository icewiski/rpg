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

namespace DungeonArchitect.Editors
{
    /// <summary>
    /// Ticks every few milli-seconds
    /// </summary>
    public class Timer
    {
        private float hertz;
        /// <summary>
        /// Ticks per second
        /// </summary>
        public float Hertz
        {
            get
            {
                return hertz;
            }
            set
            {
                hertz = value;
                if (hertz == 0)
                {
                    hertz = 1e-6f;
                }
                frameTime = 1.0f / hertz;
            }
        }

        float frameTime;
        float timeSinceFrameStart = 0;
        public delegate void OnTick(float elapsedTime);
        public event OnTick Tick;

        public Timer()
        {
            Hertz = 30;
        }

        /// <summary>
        /// Update should be called once per frame
        /// </summary>
        /// <param name="deltaSeconds">The frame time between calls</param>
        public void Update(float deltaSeconds)
        {
            timeSinceFrameStart += deltaSeconds;
            if (timeSinceFrameStart >= frameTime)
            {
                timeSinceFrameStart = 0;
                if (Tick != null)
                {
                    Tick(frameTime);
                }
            }
        }
    }
}
