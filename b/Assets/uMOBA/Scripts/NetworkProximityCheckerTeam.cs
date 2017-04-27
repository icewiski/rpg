// A simple proximity check around ourselves isn't enough in games where we have
// multiple units in our team (MOBA, RTS etc.). There we want to see everything
// around us and everything around each of our team members.
//
// We also modify the NetworkProximityChecker source from BitBucket to support
// colliders on child objects by searching the NetworkIdentity in parents.
//
// Note: requires at least Unity 5.3.5, otherwise there is IL2CPP bug #786499.
// Note: visRange means 'enemies in range<visRange can see me'. it does not mean
//       'I can see enemies in visRange'. So a big visrange on a player only
//       means that others can see him, it doesn't mean that he can see far.
//       => we should use the same visrange for everything
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;

public class NetworkProximityCheckerTeam : NetworkProximityChecker {
    // some entities like towers and bases should always be visible, even in the
    // fog of war etc.
    [SerializeField] bool observedByAll = false;

    bool SeenByEnemy(HashSet<Entity> enemies) {
        return enemies.Any(e => Vector3.Distance(transform.position, e.transform.position) <= visRange);
    }

    public override bool OnRebuildObservers(HashSet<NetworkConnection> observers, bool initial) {
        // cache entity access
        var self = GetComponent<Entity>();
        if (self == null) return true; // double check to avoid bug #786248

        // add self in any case
        var uvSelf = GetComponent<NetworkIdentity>();
        if (uvSelf == null) return true; // double check to avoid bug #786248
        if (uvSelf.connectionToClient != null)
            observers.Add(uvSelf.connectionToClient);

        // force hidden? then we are done here (we only have to ensure that the
        // player can still see itself)
        if (forceHidden) return true;        

        // everyone in our team can see us
        foreach (var p in Entity.teams[self.team]) {
            var uv = p.GetComponent<NetworkIdentity>();
            if (uv != null && uv.connectionToClient != null)
                observers.Add(uv.connectionToClient);
        }
        
        // are we observed by all OR does at least one enemy player/minion/...
        // see us? then add the whole enemy team
        var enemies = Entity.teams[self.team == Team.Evil ? Team.Good : Team.Evil];
        if (observedByAll || SeenByEnemy(enemies)) {
            foreach (var e in enemies) {
                var uv = e.GetComponent<NetworkIdentity>();
                if (uv != null && uv.connectionToClient != null)
                    observers.Add(uv.connectionToClient);
            }
        }

        return true;
    }

    void OnDrawGizmos() {
        // draw visRange vor debug reasons
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, visRange);
    }
}
