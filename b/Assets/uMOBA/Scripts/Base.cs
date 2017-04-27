// The Base entity type, used for buildings that can be destroyed.
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

[RequireComponent(typeof(Animator))]
public class Base : Entity {
    [Header("Health")]
    [SerializeField] int _healthMax = 1;
    public override int healthMax { get { return _healthMax; } }
    public override int manaMax { get { return 0; } }

    // other properties
    [Header("Defense")]
    [SyncVar, SerializeField] int baseDefense = 1;
    public override int defense { get { return baseDefense; } }

    public override int damage { get { return 0; } }

    [SyncVar, SerializeField] int baseBlockChance = 0;
    public override float blockChance { get { return baseBlockChance; } }

    public override float criticalChance { get { return 0; } }

    [Header("Death")]
    [SerializeField] GameObject deathEffect;
    
    // networkbehaviour ////////////////////////////////////////////////////////
    public override void OnStartServer() {
        base.OnStartServer();

        // all buildings should spawn with full health and mana
        health = healthMax;
        mana = manaMax;
    }

    [ClientCallback] // no need to do animations on the server
    void LateUpdate() {
        // pass parameters to animation state machine
        // => passing the states directly is the most reliable way to avoid all
        //    kinds of glitches like movement sliding, attack twitching, etc.
        animator.SetBool("DEAD", state == "DEAD");
    }

    // finite state machine states /////////////////////////////////////////////
    [Server] protected override string UpdateServer() {
        return health > 0 ? "IDLE" : "DEAD";
    }
    [Client] protected override void UpdateClient() {
        // did we just die? then load the death effect once
        if (health == 0 && deathEffect) {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            deathEffect = null;
        }
    }

    // skills //////////////////////////////////////////////////////////////////
    public override bool CanAttackType(System.Type type) { return false; }
}
