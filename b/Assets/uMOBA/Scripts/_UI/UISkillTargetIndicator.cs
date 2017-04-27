using UnityEngine;
using System.Collections;

public class UISkillTargetIndicator : MonoBehaviour {
    public GameObject indicator;

    void Update() {
        var player = Utils.ClientLocalPlayer();
        if (!player) return;

        indicator.SetActive(player.wantedSkill != -1);
        indicator.transform.position = Input.mousePosition;
    }
}
