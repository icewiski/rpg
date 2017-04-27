using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class UIHealthMana : MonoBehaviour {
    [SerializeField] Slider healthSlider;
    [SerializeField] Text healthStatus;
    [SerializeField] Slider manaSlider;
    [SerializeField] Text manaStatus;

    void Update() {
        var player = Utils.ClientLocalPlayer();
        if (!player) return;

        healthSlider.value = player.HealthPercent();
        healthStatus.text = player.health + " / " + player.healthMax;

        manaSlider.value = player.ManaPercent();
        manaStatus.text = player.mana + " / " + player.manaMax;
    }
}
