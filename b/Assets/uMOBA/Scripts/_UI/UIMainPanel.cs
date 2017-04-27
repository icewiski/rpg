using UnityEngine;
using UnityEngine.UI;

public class UIMainPanel : MonoBehaviour {
    [SerializeField] Image portrait;

    void Update() {
        var player = Utils.ClientLocalPlayer();
        if (!player) return;

        portrait.sprite = player.portrait;
    }
}
