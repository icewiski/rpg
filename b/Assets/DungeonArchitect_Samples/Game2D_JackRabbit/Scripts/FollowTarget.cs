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

public class FollowTarget : MonoBehaviour {
	public Transform target;
	public float sensitivity = 1;

	void Awake() {
		SetTarget(target.position);
	}

	// Update is called once per frame
	void Update () {
		var current = gameObject.transform.position;
		var desired = target.position;
		var dt = Mathf.Min (Time.deltaTime, 0.1f);
		var t = sensitivity * dt;
		SetTarget(Vector3.Lerp(current, desired, t));
	}

	void SetTarget(Vector3 position3D) {
		var position = gameObject.transform.position;
		position.x = position3D.x;
		position.y = position3D.y;
		gameObject.transform.position = position;
	}
}
