// The Tower entity type. Automatically attacks entities from the opposite team.
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

public class Tower : Entity {
    [Header("Health")]
    [SerializeField] int _healthMax = 200;
    public override int healthMax { get { return _healthMax; } }
    public override int manaMax { get { return 0; } }

    // other properties
    [Header("Damage & Defense")]
    [SyncVar, SerializeField] int baseDamage = 1;
    public override int damage { get { return baseDamage; } }

    [SyncVar, SerializeField] int baseDefense = 1;
    public override int defense { get { return baseDefense; } }

    [SyncVar, SerializeField] int baseBlockChance = 0;
    public override float blockChance { get { return baseBlockChance; } }

    [SerializeField, Range(0, 1)] float baseCritChance = 0;
    public override float criticalChance { get { return baseCritChance; } }

    [Header("Reward")]
    public long rewardExperience = 20;
    public int rewardGold = 20;
    
    // networkbehaviour ////////////////////////////////////////////////////////
    public override void OnStartServer() {
        base.OnStartServer();

        // all towers should spawn with full health and mana
        health = healthMax;
        mana = manaMax;
    }

    // finite state machine events /////////////////////////////////////////////
    bool EventDied() {
        return health == 0;
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
        if (EventTargetTooFarToAttack()) {
            // invalid target. stop trying to cast.
            target = null;
            currentSkill = -1;
            return "IDLE";
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
        if (EventTargetDied()) {} // don't care
        if (EventTargetDisappeared()) {} // don't care
        if (EventSkillFinished()) {} // don't care

        return "IDLE"; // nothing interesting happened
    }
    
    [Server]
    string UpdateServer_CASTING() {
        // events sorted by priority (e.g. target doesn't matter if we died)
        if (EventDied()) {
            // we died.
            OnDeath();
            currentSkill = -1; // in case we died while trying to cast
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
        if (EventTargetTooFarToAttack()) {} // don't care, we were close enough when starting to cast
        if (EventAggro()) {} // don't care, always have aggro while casting
        if (EventSkillRequest()) {} // don't care, that's why we are here
        
        return "CASTING"; // nothing interesting happened
    }
    
    [Server]
    string UpdateServer_DEAD() {
        // events sorted by priority (e.g. target doesn't matter if we died)
        if (EventSkillRequest()) {} // don't care
        if (EventSkillFinished()) {} // don't care
        if (EventTargetDisappeared()) {} // don't care
        if (EventTargetDied()) {} // don't care
        if (EventTargetTooFarToAttack()) {} // don't care
        if (EventAggro()) {} // don't care
        if (EventDied()) {} // don't care
        return "DEAD";
    }

    [Server]
    protected override string UpdateServer() {
        if (state == "IDLE")    return UpdateServer_IDLE();
        if (state == "CASTING") return UpdateServer_CASTING();
        if (state == "DEAD")    return UpdateServer_DEAD();
        Debug.LogError("invalid state:" + state);
        return "IDLE";
    }

    // finite state machine - client ///////////////////////////////////////////
    [Client]
    protected override void UpdateClient() {}

    [Server]
    void OnDeath() {
        // disappear forever
        NetworkServer.Destroy(gameObject);
    }

    // aggro ///////////////////////////////////////////////////////////////////
    [ServerCallback] // called by AggroArea from servers and clients
    public override void OnAggro(Entity entity) {
        // alive? (dead entities have colliders too) and different team?
        if (entity && entity.health > 0 && entity.team != team && CanAttackType(entity.GetType())) {
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

    // skills //////////////////////////////////////////////////////////////////
    public override bool CanAttackType(System.Type type) {
        return type == typeof(Monster) || type == typeof(Player);
    }
}
