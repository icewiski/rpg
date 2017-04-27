// The Npc class is rather simple. It contains state Update functions that do
// nothing at the moment, because Npcs are supposed to stand around all day.
//
// Npcs first show the welcome text and then have options for item trading and
// quests.
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

public class Npc : Entity {
    [Header("Health")]
    [SerializeField] int _healthMax = 1;
    public override int healthMax { get { return _healthMax; } }

    [Header("Mana")]
    [SerializeField] int _manaMax = 1;
    public override int manaMax { get { return _manaMax; } }

    [Header("Items for Sale")]
    public ItemTemplate[] saleItems;

    // other properties
    public override int damage { get { return 0; } }
    public override int defense { get { return 0; } }
    public override float blockChance { get { return 0; } }
    public override float criticalChance { get { return 0; } }
    
    // networkbehaviour ////////////////////////////////////////////////////////
    public override void OnStartServer() {
        base.OnStartServer();

        // all npcs should spawn with full health and mana
        health = healthMax;
        mana = manaMax;
    }

    // finite state machine states /////////////////////////////////////////////
    [Server] protected override string UpdateServer() { return state; }
    [Client] protected override void UpdateClient() {}

    // skills //////////////////////////////////////////////////////////////////
    public override bool CanAttackType(System.Type type) { return false; }
}
