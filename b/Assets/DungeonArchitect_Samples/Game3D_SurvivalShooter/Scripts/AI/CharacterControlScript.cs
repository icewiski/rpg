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


namespace DAShooter
{
	public abstract class CharacterControlScript : MonoBehaviour {
		protected StateMachine stateMachine;


		// Use this for initialization
		void Start () {
			stateMachine = new StateMachine();

			Initialize ();
		}

		protected virtual void Initialize() {}
		
		// Update is called once per frame
		void FixedUpdate () {

		}

		void Update() {
			stateMachine.Update();
		}

		public abstract bool GetInputJump();
		public abstract bool GetInputAttackPrimary();
		public abstract bool IsGrounded();
		public abstract void ApplyMovement(Vector3 velocity);
	}
}
