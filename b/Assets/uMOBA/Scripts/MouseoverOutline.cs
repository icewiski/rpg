// Draws an outline around the entity while the mouse hovers over it. The
// outline strength can be set in the entity's material.
// Note: requires a shader with "_OutlineColor" parameter.
// Note: we use the outline color alpha channel for visibility, which is easier
// than saving the default strenghts and settings strengths to 0.
using UnityEngine;

[RequireComponent(typeof(Entity))]
public class MouseoverOutline : MonoBehaviour {
    [SerializeField] Color colorTeam = Color.green;
    [SerializeField] Color colorEnemy = Color.red;

    void SetOutline(Color col) {
        foreach (var r in GetComponentsInChildren<Renderer>())
            foreach (var mat in r.materials)
                if (mat.HasProperty("_OutlineColor"))
                    mat.SetColor("_OutlineColor", col);
    }

    void Awake() {
        // disable outline by default once
        SetOutline(Color.clear);
    }

    void OnMouseEnter() {
        // only for clients, and not for local player
        var entity = GetComponent<Entity>();
        if (entity.isClient && !entity.isLocalPlayer) {
            // find local player
            var go = Utils.ClientLocalPlayer();
            if (go != null) {
                // only if the local player is currently selectling a skill
                var p = go.GetComponent<Player>();
                if (p != null) {
                    // our color depends on local player's team
                    SetOutline(entity.team == p.team ? colorTeam : colorEnemy);
                }
            }
        }
    }
    
    void OnMouseExit() {
        // disable outline
        SetOutline(Color.clear);
    }
}
