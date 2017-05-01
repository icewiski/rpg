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
using DungeonArchitect;
using DungeonArchitect.Utils;
using DungeonArchitect.Graphs;

using DAShooter;

public class MiniMapGenerator : MonoBehaviour {
	public List<Graph> miniMapThemes;
	GameObject miniMapDungeonObject;
	Dungeon minimapDungeon;

	// Use this for initialization
	public void BuildMiniMap(Dungeon baseDungeon) {
		if (miniMapDungeonObject == null) {
			miniMapDungeonObject = Instantiate(baseDungeon.gameObject);
		}

		// Move the mini-map dungeon down
		minimapDungeon = miniMapDungeonObject.GetComponent<Dungeon>();
		minimapDungeon.transform.position = gameObject.transform.position;

		// Disable unwanted components from the cloned minimap dungeon
		DisableComponent<WaypointGenerator>(miniMapDungeonObject);
		DisableComponent<LevelNpcSpawner>(miniMapDungeonObject);
		DisableComponent<SpecialRoomFinder>(miniMapDungeonObject);
		DisableComponent<MiniMapRebuilder>(miniMapDungeonObject);

		// Apply the mini-map themes and rebuild
		minimapDungeon.dungeonThemes = miniMapThemes;
		minimapDungeon.Config.Seed = baseDungeon.Config.Seed;
		minimapDungeon.Build();
	}

	public void DestroyDungeon() {
		if (minimapDungeon != null) {
			minimapDungeon.DestroyDungeon();
		}

	}

	void DisableComponent<T>(GameObject obj) where T : MonoBehaviour {
		var component = obj.GetComponent<T>();
		if (component != null) {
			component.enabled = false;
		}
	}
}
