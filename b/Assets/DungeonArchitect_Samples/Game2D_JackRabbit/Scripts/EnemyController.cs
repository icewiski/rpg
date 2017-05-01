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
using DungeonArchitect.Navigation;

namespace JackRabbit {
	public class EnemyController : MonoBehaviour {

		public Animator animator;
		public float maxHealth;

		bool facingRight = true;
		float currentHealth;


		Rigidbody2D rigidBody2D;
		void Awake() {
			rigidBody2D = GetComponent<Rigidbody2D>();
			currentHealth = maxHealth;
		}

		// Update is called once per frame
		void FixedUpdate () {
			animator.SetFloat("Speed", rigidBody2D.velocity.magnitude);

			var moveX = rigidBody2D.velocity.x;
			if (moveX > 0 && facingRight) {
				Flip();
			} else if (moveX < 0 && !facingRight) {
				Flip ();
			}

		}
		
		void Flip() {
			facingRight = !facingRight;
			var scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale;
		}

		public bool Alive {
			get { return currentHealth > 0; }
		}

		public void ApplyDamage(float amount) {
			if (Alive) {
				currentHealth -= amount;
				if (!Alive) {
					OnDead();
				}
			}
		}

		void OnDead() {
			animator.SetTrigger("Dead");
			rigidBody2D.velocity = Vector2.zero;
			rigidBody2D.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
			var colliders = GetComponents<Collider2D>();
			foreach (var collider in colliders) {
				collider.enabled = false;
			}

			
			GetComponent<DungeonNavAgent>().enabled = false;
			GetComponent<DAShooter.AIController>().enabled = false;
		}

	}
}
