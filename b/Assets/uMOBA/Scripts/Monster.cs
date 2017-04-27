// The Monster class has a few different features that all aim to make monsters
// behave as realistically as possible.
//
// - **States:** first of all, the monster has several different states like
// IDLE, ATTACKING, MOVING and DEATH. The monster will randomly move around in
// a certain movement radius and try to attack any players in its aggro range.
// _Note: monsters use NavMeshAgents to move on the NavMesh._
//
// - **Aggro:** To save computations, we let Unity take care of finding players
// in the aggro range by simply adding a AggroArea _(see AggroArea.cs)_ sphere
// to the monster's children in the Hierarchy. We then use the OnTrigger
// functions to find players that are in the aggro area. The monster will always
// move to the nearest aggro player and then attack it as long as the player is
// in the follow radius. If the player happens to walk out of the follow
// radius then the monster will continue to walk to its goal.
//
// - **Respawning:** The monsters have a _respawn_ property that can be set to
// true in order to make the monster respawn after it died. We developed the
// respawn system with simplicity in mind, there are no extra spawner objects
// needed. As soon as a monster dies, it will make itself invisible for a while
// and then go back to the starting position to respawn. This feature allows the
// developer to quickly drag monster Prefabs into the scene and place them
// anywhere, without worrying about spawners and spawn areas.
//
// - **Loot:** Dead monsters can also generate loot, based on the _lootGold_,
// which generates gold between a minimum and a maximum amount.
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Networking;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
public class Monster : Entity {
    [Header("Health")]
    [SerializeField] int _healthMax = 1;
    public override int healthMax { get { return _healthMax; } }

    [Header("Mana")]
    [SerializeField] int _manaMax = 1;
    public override int manaMax { get { return _manaMax; } }

    [Header("Damage")]
    [SerializeField] int _damage = 2;
    public override int damage { get { return _damage; } }
    
    [Header("Defense")]
    [SerializeField] int _defense = 1;
    public override int defense { get { return _defense; } }

    [Header("Block")]
    [SerializeField, Range(0, 1)] float _blockChance = 0;
    public override float blockChance { get { return _blockChance; } }

    [Header("Critical")]
    [SerializeField, Range(0, 1)] float _criticalChance = 0;
    public override float criticalChance { get { return _criticalChance; } }
    
    [Header("Movement")]
    // monsters should follow their targets even if they run out of the movement
    // radius. the follow dist should always be bigger than the biggest archer's
    // attack range, so that archers will always pull aggro, even when attacking
    // from far away.
    [SerializeField] float followDistance = 10;
    // the movement goal (enemy base or spawn point for forest monsters)
    [HideInInspector] public Transform goal;

    [Header("Reward")]
    public long rewardExperience = 10;
    public int rewardGold = 10;

    [Header("Respawn")]
    [SerializeField] float deathTime = 30f; // enough for animation
    float deathTimeEnd;
    [SerializeField] bool respawn = true;
    [SerializeField] float respawnTime = 10f;
    float respawnTimeEnd;

    // save the start position for random movement distance and respawning
    Vector3 startPosition;

    // networkbehaviour ////////////////////////////////////////////////////////
    protected override void Awake() {
        base.Awake();
        startPosition = transform.position;
    }

    public override void OnStartServer() {
        // call Entity's OnStartServer
        base.OnStartServer();

        // all monsters should spawn with full health and mana
        health = healthMax;
        mana = manaMax;
    }

    [ClientCallback] // no need to do animations on the server
    void LateUpdate() {
        // pass parameters to animation state machine
        // => passing the states directly is the most reliable way to avoid all
        //    kinds of glitches like movement sliding, attack twitching, etc.
        // => make sure to import all looping animations like idle/run/attack
        //    with 'loop time' enabled, otherwise the client might only play it
        //    once
        // => only play moving animation while the agent is actually moving. the
        //    MOVING state might be delayed to due latency or we might be in
        //    MOVING while a path is still pending, etc.
        animator.SetBool("MOVING", state == "MOVING" && agent.velocity != Vector3.zero);
        animator.SetBool("CASTING", state == "CASTING");
        animator.SetInteger("currentSkill", currentSkill);
        animator.SetBool("DEAD", state == "DEAD");
    }

    // OnDrawGizmos only happens while the Script is not collapsed
    void OnDrawGizmos() {
        // draw the follow dist (based on start for forest monster, pos otherwise)
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(goal != null ? transform.position : startPosition, followDistance);
    }

    // finite state machine events /////////////////////////////////////////////
    bool EventDied() {
        return health == 0;
    }

    bool EventDeathTimeElapsed() {
        return state == "DEAD" && Time.time >= deathTimeEnd;
    }

    bool EventRespawnTimeElapsed() {
        return state == "DEAD" && respawn && Time.time >= respawnTimeEnd;
    }

    bool EventTargetDisappeared() {
        return target == null;
    }

    bool EventTargetDied() {
        return target != null && target.health == 0;
    }

    bool EventTargetTooFarToAttack() {
        return target != null &&
               0 <= currentSkill && currentSkill < skills.Count &&
               !CastCheckDistance(skills[currentSkill]);
    }

    bool EventTargetTooFarToFollow() {
        // for forest monsters the distance is between start and target. for
        // lane monsters it's between monster and target
        if (goal != null)
            return target != null &&
                   Utils.ClosestDistance(collider, target.collider) > followDistance;
        else
            return target != null &&
                   Vector3.Distance(startPosition, target.collider.ClosestPointOnBounds(transform.position)) > followDistance;
    }

    bool EventAggro() {
        return target != null && target.health > 0;
    }
    
    bool EventSkillRequest() {
        return 0 <= currentSkill && currentSkill < skills.Count;        
    }
    
    bool EventSkillFinished() {
        return 0 <= currentSkill && currentSkill < skills.Count &&
               skills[currentSkill].CastTimeRemaining() == 0;        
    }

    bool EventMoveEnd() {
        return state == "MOVING" && !IsMoving();
    }

    // finite state machine - server ///////////////////////////////////////////
    [Server]
    string UpdateServer_IDLE() {
        // events sorted by priority (e.g. target doesn't matter if we died)
        if (EventDied()) {
            // we died.
            OnDeath();
            currentSkill = -1; // in case we died while trying to cast
            return "DEAD";
        }
        if (EventTargetDied()) {
            // we had a target before, but it died now. clear it.
            target = null;
            currentSkill = -1;
            return "IDLE";
        }
        if (EventTargetTooFarToFollow()) {
            // we had a target before, but it's out of follow range now.
            // clear it in any case
            target = null;
            currentSkill = -1;

            // if we have a goal, just go to idle and the rest will work out.
            if (goal != null) {
                return "IDLE";
            // if we don't then walk back to start (for forest monsters)
            } else {                
                agent.stoppingDistance = 0;
                agent.destination = startPosition;
                return "MOVING";
            }
        }
        if (EventTargetTooFarToAttack()) {
            // we had a target before, but it's out of attack range now.
            // follow it. (use collider point(s) to also work with big entities)
            agent.stoppingDistance = CurrentCastRange() * 0.8f;
            agent.destination = target.collider.ClosestPointOnBounds(transform.position);
            return "MOVING";
        }
        if (EventSkillRequest()) {
            // we had a target in attack range before and trying to cast a skill
            // on it. check self (alive, mana, weapon etc.) and target
            var skill = skills[currentSkill];
            if (CastCheckSelf(skill) && CastCheckTarget(skill)) {
                // start casting and set the casting end time
                skill.castTimeEnd = Time.time + skill.castTime;
                skills[currentSkill] = skill;
                return "CASTING";
            } else {
                // invalid target. stop trying to cast.
                target = null;
                currentSkill = -1;
                return "IDLE";
            }
        }
        if (EventAggro()) {
            // target in attack range. try to cast a first skill on it
            if (skills.Count > 0) currentSkill = 0;
            else Debug.LogError(name + " has no skills to attack with.");
            return "IDLE";
        }
        if (EventDeathTimeElapsed()) {} // don't care
        if (EventRespawnTimeElapsed()) {} // don't care
        if (EventMoveEnd()) {} // don't care
        if (EventSkillFinished()) {} // don't care
        if (EventTargetDisappeared()) {} // don't care

        // if nothing interesting happened: keep moving towards the goal (if any)
        if (goal != null) {
            agent.stoppingDistance = 0;
            agent.destination = goal.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
            return "MOVING";
        } else return "IDLE";
    }
    
    [Server]
    string UpdateServer_MOVING() {
        // events sorted by priority (e.g. target doesn't matter if we died)
        if (EventDied()) {
            // we died.
            OnDeath();
            currentSkill = -1; // in case we died while trying to cast
            return "DEAD";
        }
        if (EventMoveEnd()) {
            // we reached our destination.
            return "IDLE";
        }
        if (EventTargetDied()) {
            // we had a target before, but it died now. clear it.
            target = null;
            currentSkill = -1;
            agent.ResetPath();
            return "IDLE";
        }
        if (EventTargetTooFarToFollow()) {
            // we had a target before, but it's out of follow range now.
            // clear it in any case
            target = null;
            currentSkill = -1;

            // if we have a goal, just go to idle and the rest will work out.
            if (goal != null) {
                agent.ResetPath();
                return "IDLE";
            // if we don't then walk back to start (for forest monsters)
            } else {                
                agent.stoppingDistance = 0;
                agent.destination = startPosition;
                return "MOVING";
            }
        }
        if (EventTargetTooFarToAttack()) {
            // we had a target before, but it's out of attack range now.
            // follow it. (use collider point(s) to also work with big entities)
            agent.stoppingDistance = CurrentCastRange() * 0.8f;
            agent.destination = target.collider.ClosestPointOnBounds(transform.position);
            return "MOVING";
        }
        if (EventAggro()) {
            // target in attack range. try to cast a first skill on it
            // (we may get a target while randomly wandering around)
            if (skills.Count > 0) currentSkill = 0;
            else Debug.LogError(name + " has no skills to attack with.");
            agent.ResetPath();
            return "IDLE";
        }
        if (EventDeathTimeElapsed()) {} // don't care
        if (EventRespawnTimeElapsed()) {} // don't care
        if (EventSkillFinished()) {} // don't care
        if (EventTargetDisappeared()) {} // don't care
        if (EventSkillRequest()) {} // don't care, finish movement first
        
        return "MOVING"; // nothing interesting happened
    }
    
    [Server]
    string UpdateServer_CASTING() {
        // keep looking at the target for server & clients (only Y rotation)
        if (target) LookAtY(target.transform.position);

        // events sorted by priority (e.g. target doesn't matter if we died)
        if (EventDied()) {
            // we died.
            OnDeath();
            currentSkill = -1; // in case we died while trying to cast
            agent.ResetPath();
            return "DEAD";
        }
        if (EventTargetDisappeared()) {
            // target disappeared, stop casting
            currentSkill = -1;
            target = null;
            return "IDLE";
        }
        if (EventTargetDied()) {
            // target died, stop casting
            currentSkill = -1;
            target = null;
            return "IDLE";
        }
        if (EventSkillFinished()) {
            // finished casting. apply the skill on the target.
            CastSkill(skills[currentSkill]);

            // did the target die? then clear it so that the monster doesn't
            // run towards it if the target respawned
            if (target.health == 0) target = null;
            
            // go back to IDLE
            currentSkill = -1;
            return "IDLE";
        }
        if (EventDeathTimeElapsed()) {} // don't care
        if (EventRespawnTimeElapsed()) {} // don't care
        if (EventMoveEnd()) {} // don't care
        if (EventTargetTooFarToFollow()) {} // don't care, we were close enough when starting to cast
        if (EventTargetTooFarToAttack()) {} // don't care, we were close enough when starting to cast
        if (EventAggro()) {} // don't care, always have aggro while casting
        if (EventSkillRequest()) {} // don't care, that's why we are here
        
        return "CASTING"; // nothing interesting happened
    }
    
    [Server]
    string UpdateServer_DEAD() {
        // events sorted by priority (e.g. target doesn't matter if we died)
        if (EventRespawnTimeElapsed()) {
            // respawn at the start position with full health, visibility
            Show();
            agent.Warp(startPosition); // recommended over transform.position
            Revive();
            return "IDLE";
        }
        if (EventDeathTimeElapsed()) {
            // we were lying around dead for long enough now.
            // hide while respawning, or disappear forever
            if (respawn) Hide();
            else NetworkServer.Destroy(gameObject);
            return "DEAD";
        }
        if (EventSkillRequest()) {} // don't care
        if (EventSkillFinished()) {} // don't care
        if (EventMoveEnd()) {} // don't care
        if (EventTargetDisappeared()) {} // don't care
        if (EventTargetDied()) {} // don't care
        if (EventTargetTooFarToAttack()) {} // don't care
        if (EventTargetTooFarToFollow()) {} // don't care
        if (EventAggro()) {} // don't care
        if (EventDied()) {} // don't care, of course we are dead
        
        return "DEAD"; // nothing interesting happened
    }

    [Server]
    protected override string UpdateServer() {
        if (state == "IDLE")    return UpdateServer_IDLE();
        if (state == "MOVING")  return UpdateServer_MOVING();
        if (state == "CASTING") return UpdateServer_CASTING();
        if (state == "DEAD")    return UpdateServer_DEAD();
        Debug.LogError("invalid state:" + state);
        return "IDLE";
    }

    // finite state machine - client ///////////////////////////////////////////
    [Client]
    protected override void UpdateClient() {
        if (state == "CASTING") {            
            // keep looking at the target for server & clients (only Y rotation)
            if (target) LookAtY(target.transform.position);
        }
    }

    // aggro ///////////////////////////////////////////////////////////////////
    [ServerCallback] // called by AggroArea from servers and clients
    public override void OnAggro(Entity entity) {
        // are we alive, and is the entity alive and of correct type and team?
        if (health > 0 && entity != null && entity.health > 0 && entity.team != team && CanAttackType(entity.GetType())) {
            // no target yet(==self), or closer than current target?
            // => has to be at least 20% closer to be worth it, otherwise we
            //    may end up nervously switching between two targets
            // => we do NOT use Utils.ClosestDistance, because then we often
            //    also end up nervously switching between two animated targets,
            //    since their collides moves with the animation.
            //    => we don't even need closestdistance here because they are in
            //       the aggro area anyway. transform.position is perfectly fine
            if (target == null) {
                target = entity;
            } else {
                float oldDistance = Vector3.Distance(transform.position, target.transform.position);
                float newDistance = Vector3.Distance(transform.position, entity.transform.position);
                if (newDistance < oldDistance * 0.8) target = entity;
            }
        }
    }

    // death ///////////////////////////////////////////////////////////////////
    [Server]
    void OnDeath() {
        // set death and respawn end times. we set both of them now to make sure
        // that everything works fine even if a monster isn't updated for a
        // while. so as soon as it's updated again, the death/respawn will
        // happen immediately if current time > end time.
        deathTimeEnd = Time.time + deathTime;
        respawnTimeEnd = deathTimeEnd + respawnTime; // after death time ended
        
        // stop buffs, clear target
        StopBuffs();
        target = null;
    }

    // skills //////////////////////////////////////////////////////////////////
    public override bool CanAttackType(System.Type type) {
        return type == typeof(Monster) || type == typeof(Player) || type == typeof(Tower) || type == typeof(Barrack) || type == typeof(Base);
    }

    // helper function to get the current cast range (if casting anything)
    public float CurrentCastRange() {
        return 0 <= currentSkill && currentSkill < skills.Count ? skills[currentSkill].castRange : 0;
    }
}
