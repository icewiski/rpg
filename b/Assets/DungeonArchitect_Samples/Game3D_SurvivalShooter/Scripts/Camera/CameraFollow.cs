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

namespace DAShooter
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;            // The position that that camera will be following.
		public float positionSmoothing = 5f;        // The speed with which the camera will be following.
		public float lookAtSmoothing = 5f;        // The speed with which the camera will be following.
        public Vector3 offset;                     // The initial offset from the target.

		Vector3 currentLookAt;

        void Start ()
        {
            // Calculate the initial offset.
            //offset = transform.position - target.position;
			currentLookAt = target.position;
        }


        void FixedUpdate ()
        {
            // Create a postion the camera is aiming for based on the offset from the target.
            Vector3 targetCamPos = target.position + offset;

			// Smoothly interpolate between the camera's current position and it's target position.
			transform.position = Vector3.Lerp (transform.position, targetCamPos, positionSmoothing * Time.deltaTime);
			currentLookAt = Vector3.Lerp (currentLookAt, target.position, lookAtSmoothing * Time.deltaTime);
			transform.LookAt(currentLookAt);
		}
    }
}
