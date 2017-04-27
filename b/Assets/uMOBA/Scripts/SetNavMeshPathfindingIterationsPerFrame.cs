using UnityEngine;
using UnityEngine.AI;

public class SetNavMeshPathfindingIterationsPerFrame : MonoBehaviour {
    [SerializeField] int iterations = 100; // default

    void Awake() {
        print("Setting NavMesh Pathfinding Iterations Per Frame from " + NavMesh.pathfindingIterationsPerFrame + " to " + iterations);
        NavMesh.pathfindingIterationsPerFrame = iterations;
    }
}
