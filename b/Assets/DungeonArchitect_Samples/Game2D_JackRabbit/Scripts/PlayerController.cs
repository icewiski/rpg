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

namespace JackRabbit {
	public class PlayerController : MonoBehaviour {
		public float maxSpeed = 5;
		public float attackMoveSpeedMultiplier = 0.1f;
		public float sprintMultiplier = 1.5f;
		public float movementSensitivity = 0.1f;
		public float attackStength = 40;

		bool facingRight = true;
		Rigidbody2D rigidBody2D;
		Animator animator;
		bool attacking = false;


		void Awake() {
			rigidBody2D = GetComponent<Rigidbody2D>();
			animator = GetComponent<Animator>();
		}

		void FixedUpdate () {
			float moveX = Input.GetAxis("Horizontal");
			float moveY = Input.GetAxis("Vertical");
			
			attacking = Input.GetButton("Fire1");
			animator.SetBool("Attack", attacking);

			var sprintPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
			var targetSpeed = maxSpeed;
			if (attacking) {
				targetSpeed *= attackMoveSpeedMultiplier;
			}
			else if (sprintPressed) {
				targetSpeed *= sprintMultiplier;
			}
			var direction = new Vector2(moveX, moveY);
			var directionLength = direction.magnitude;
			if (directionLength > 1) {
				direction /= directionLength;
			}

			var currentSpeed = rigidBody2D.velocity.magnitude;
			var desiredSpeed = Mathf.Lerp(currentSpeed, targetSpeed, movementSensitivity);

			rigidBody2D.velocity = direction * desiredSpeed;
			
			if (moveX > 0 && !facingRight) {
				Flip();
			} else if (moveX < 0 && facingRight) {
				Flip ();
			}

			animator.SetFloat("Speed", rigidBody2D.velocity.magnitude);
		}

		void OnAttack() {
			var offset = new Vector3(0.3f, 0.7f, 0);
			offset.x *= Mathf.Sign (transform.localScale.x);
			var radius = 0.7f;
			var colliders = Physics2D.OverlapCircleAll(gameObject.transform.position + offset, radius);
			foreach (var collider in colliders) {
                var enemyController = collider.gameObject.GetComponent<DAShooter.AIController>();
                if (enemyController != null)
                {
					// Apply damage to the enemy
					var enemy = collider.gameObject.GetComponent<EnemyController>();
					if (enemy != null) {
						enemy.ApplyDamage(attackStength);
					}
				}
			}
		}

		void Update() {
		}

		void Flip() {
			facingRight = !facingRight;
			var scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale;
		}
	}
}
