// Used to spawn monsters repeatedly. The 'monsterGoals' will be passed to the
// monster after spawning it, so that the monster knows where to move (which
// lane to walk along).
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;
using System.Linq;

public class MonsterSpawner : NetworkBehaviour {
    // the monster to spawn
    [SerializeField] Monster monster;
    [SerializeField] float interval = 5f;
    [SerializeField] Transform monsterGoal; // passed to monsters
    [SerializeField] string NavMeshAreaPreferred = ""; // MidLane, etc.

    public override void OnStartServer() {
        InvokeRepeating("Spawn", interval, interval);
    }

    [Server]
    void Spawn() {
        var go = (GameObject)Instantiate(monster.gameObject, transform.position, Quaternion.identity);
        go.name = monster.name; // remove "(Clone)" suffix
        go.GetComponent<Monster>().goal = monsterGoal;

        // set preferred navmesh area costs to 1
        int index = NavMesh.GetAreaFromName(NavMeshAreaPreferred);
        if (index != -1)
            go.GetComponent<NavMeshAgent>().SetAreaCost(index, 1);

        NetworkServer.Spawn(go);
    }
}
