using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class UIShortcuts : MonoBehaviour {    
    [SerializeField] Button quitButton;

    void Update() {
        var player = Utils.ClientLocalPlayer();
        if (!player) return;

        quitButton.onClick.SetListener(() => {
            NetworkManager.singleton.StopClient();
            Application.Quit();
        });
    }
}
