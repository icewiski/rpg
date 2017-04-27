using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Serialization;

public class UIWinLose : MonoBehaviour {
    [SerializeField] GameObject panel;
    [SerializeField] Text winnerText;
    [SerializeField] Button quitButton;
    
    [SerializeField] Base baseGood;
    [SerializeField] Base baseEvil;

    void Update() {
        var player = Utils.ClientLocalPlayer();
        if (!player) return;

        // is there a base with 0 health?
        if (baseGood.health == 0) {
            panel.SetActive(true);
            winnerText.text = baseEvil.team.ToString();
        } else if (baseEvil.health == 0) {
            panel.SetActive(true);
            winnerText.text = baseGood.team.ToString();
        } else panel.SetActive(false); // hide

        // quit button in any case
        quitButton.onClick.SetListener(() => {
            NetworkManager.singleton.StopClient();
            Application.Quit();
        });
    }
}
