/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System.Collections;
using DungeonArchitect;


public class RegenerateDungeonLayout : MonoBehaviour {
    public Dungeon dungeon;

    /// <summary>
    /// If we have static geometry already in the level created during design time, then the pooled scene
    /// provider cannot re-use it because the editor would have performed optimizations on it and might not be able to move it
    /// This flag clears out any design time static geometry before rebuilding to avoid movement issues of static objects
    /// </summary>
    bool performCleanRebuild = true;

	// Use this for initialization
	void Start () {
        StartCoroutine(RebuildDungeon());
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(RebuildDungeon());
        }
	}

    IEnumerator RebuildDungeon()
    {
        if (dungeon != null)
        {
            if (performCleanRebuild)
            {
                // We want to remove design time data with a clean destroy since editor would allow modification of optimized static game objects
                // We want to do this only for the first time
                dungeon.DestroyDungeon();
                performCleanRebuild = false;

                // Wait for 1 frame to make sure our design time objects were destroyed
                yield return 0;
            }

            // Build the dungeon
            var config = dungeon.Config;
            if (config != null)
            {
                config.Seed = (uint)(Random.value * uint.MaxValue);
                dungeon.Build();
            }
        }
    }
}
