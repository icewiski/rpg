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

public class PlayerCameraZoom : MonoBehaviour {
	public float sensitivity = 1;
	public float zoomMultiplier = 1.2f;
	public float maxSpeed = 8;
	public Rigidbody2D rigidBody2D;

	float startingZoom;
	float targetZoom;
	Camera cam;

	void Awake() {
		cam = GetComponent<Camera>();
		startingZoom = cam.orthographicSize;
	}

	// Update is called once per frame
	void Update () {
		var speed = rigidBody2D.velocity.magnitude;
		var t = speed / maxSpeed;
		var multiplier = Mathf.Lerp (1, zoomMultiplier, t);
		targetZoom = startingZoom * multiplier;

		cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, sensitivity * Time.deltaTime);
	}
}
