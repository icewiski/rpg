// Note: this script has to be on an always-active UI parent, so that we can
// always find it from other code. (GameObject.Find doesn't find inactive ones)
using UnityEngine;
using UnityEngine.UI;

public class UIRespawn : MonoBehaviour {
    [SerializeField] GameObject panel;
    [SerializeField] Text text;

    void Update() {
        var player = Utils.ClientLocalPlayer();
        if (!player) return;

        // player dead or alive?
        if (player.health == 0) {
            panel.SetActive(true);

            // calculate the respawn time remaining for the client
            float respawnAt = player.respawnTimeEnd;
            float remaining = respawnAt - NetworkTime.time;
            text.text = remaining.ToString("F0");
        } else {
            // was active before? then we just respawned. focus cam on player.
            if (panel.activeSelf)
                Camera.main.GetComponent<CameraScrolling>().FocusOn(player.transform.position);
            panel.SetActive(false);
        }
    }
}
