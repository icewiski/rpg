/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

//$ Copyright 2016, Code Respawn Technologies Pvt Ltd - All Rights Reserved $//

using UnityEditor;
using UnityEngine;
using System.Collections;
using DungeonArchitect.Terrains;

namespace DungeonArchitect.Editors
{
    /// <summary>
    /// Custom property editor for the Landscape texture data-structure
    /// </summary>
	[CustomEditor(typeof(LandscapeTexture))]
	public class LandscapeTextureEditor : Editor {
		SerializedObject sobject;
		
		SerializedProperty diffuse;
		SerializedProperty normal;
		SerializedProperty metallic;
		SerializedProperty size;
		SerializedProperty offset;

		public void OnEnable() {
			sobject = new SerializedObject(target);
			diffuse = sobject.FindProperty("diffuse");
			normal = sobject.FindProperty("normal");
			metallic = sobject.FindProperty("metallic");
			size = sobject.FindProperty("size");
			offset = sobject.FindProperty("offset");
		}

		public override void OnInspectorGUI()
		{
			sobject.Update();
			EditorGUILayout.PropertyField(diffuse);
			EditorGUILayout.PropertyField(normal);
			EditorGUILayout.PropertyField(metallic);
			EditorGUILayout.PropertyField(size);
			EditorGUILayout.PropertyField(offset);
			sobject.ApplyModifiedProperties();
		}

	}
}
