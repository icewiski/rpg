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
using DungeonArchitect.Utils;
using STE = SharpNav.Geometry.TriangleEnumerable;
using SVector3 = SharpNav.Geometry.Vector3;
using Triangle3 = SharpNav.Geometry.Triangle3;
using PolyMesh = SharpNav.PolyMesh;
using Crowd = SharpNav.Crowds.Crowd;

namespace DungeonArchitect.Navigation {
	public class TerrainTriangleProvider : NavigationTriangleProvider {
		public float terrainCellSize = 2;
		
		public override void AddNavTriangles(List<Triangle3> triangles) {
			var terrain = Terrain.activeTerrain;
			if (terrain != null && terrain.terrainData != null) {
				AddTerrainTriangles(triangles, terrain);
			}
		}
		
		void AddTerrainTriangles(List<Triangle3> triangles, Terrain terrain) {
			var data = terrain.terrainData;
			var terrainSize = data.size;
			var terrainWidth = data.heightmapWidth;
			var terrainHeight = data.heightmapHeight;
			var terrainScale = data.heightmapScale;
			
			int incrementX = Mathf.Max (1, Mathf.RoundToInt(terrainCellSize / terrainScale.x));
			int incrementZ = Mathf.Max (1, Mathf.RoundToInt(terrainCellSize / terrainScale.z));
			
			var multiplierX = terrainSize.x / (terrainWidth - 1);
			var multiplierZ = terrainSize.z / (terrainHeight - 1);
			var heights = data.GetHeights(0, 0, terrainWidth, terrainHeight);
			
			var optimizedWidth = Mathf.Floor(terrainWidth / incrementX);
			var optimizedHeight = Mathf.Floor(terrainHeight / incrementZ);
			
			var worldHeights = new SVector3[terrainWidth, terrainHeight];
			var resolution = terrain.terrainData.size.y;
			int ox = 0;
			for (var hx = 0; hx < terrainWidth; hx += incrementX) {
				int oz = 0;
				for (var hz = 0; hz < terrainHeight; hz += incrementZ) {
					var x = hx * multiplierX + terrain.transform.position.x;
					var z = hz * multiplierZ + terrain.transform.position.z;
					var y = heights[hx, hz] * resolution + terrain.transform.position.y;
					worldHeights[ox, oz] = new SVector3(z, y, x);
					oz++;
				}
				ox++;
			}
			
			var vertices = new SVector3[4];
			for (var hx = 0; hx < optimizedWidth - 1; hx++) {
				for (var hz = 0; hz < optimizedHeight - 1; hz++) {
					vertices[0] = worldHeights[hx, hz];
					vertices[1] = worldHeights[hx, hz + 1];
					vertices[2] = worldHeights[hx + 1, hz + 1];
					vertices[3] = worldHeights[hx + 1, hz];
					
					triangles.Add (new Triangle3(
						vertices[0],
						vertices[1],
						vertices[2]
						));
					
					triangles.Add (new Triangle3(
						vertices[2],
						vertices[3],
						vertices[0]
						));
				}
			}
		}
	}
}
