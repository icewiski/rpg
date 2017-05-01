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

public class CameraMovement : MonoBehaviour {
	public float movementSpeed = 15;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float forward = Input.GetAxis ("Vertical"); 
		float right = Input.GetAxis ("Horizontal"); ;
		var distance = movementSpeed * Time.deltaTime;

		// forward movement
		gameObject.transform.position += transform.forward * distance * forward;

		// strafe movement
		gameObject.transform.position += transform.right * distance * right;
	}
}
