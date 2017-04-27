using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour {
    [SerializeField] UIInventorySlot slotPrefab;
    [SerializeField] Transform content;
    [SerializeField] Text goldText;

    void Update() {
        var player = Utils.ClientLocalPlayer();
        if (!player) return;

        // instantiate/destroy enough slots
        UIUtils.BalancePrefabs(slotPrefab.gameObject, player.inventory.Count, content);

        // refresh all
        for (int i = 0; i < player.inventory.Count; ++i) {
            var slot = content.GetChild(i).GetComponent<UIInventorySlot>();
            slot.dragAndDropable.name = i.ToString(); // drag and drop index
            var item = player.inventory[i];

            // overlay hotkey (without 'Alpha' etc.)
            slot.hotKeyText.text = player.inventoryHotkeys[i].ToString().Replace("Alpha", "");

            if (item.valid) {
                // refresh valid item
                int icopy = i; // needed for lambdas, otherwise i is Count
                slot.button.onClick.SetListener(() => {
                        player.CmdUseInventoryItem(icopy);
                });
                // hotkey pressed and not typing in any input right now?
                if (Input.GetKeyDown(player.inventoryHotkeys[i]) && !UIUtils.AnyInputActive())
                    player.CmdUseInventoryItem(i);
                slot.tooltip.enabled = true;
                slot.tooltip.text = item.ToolTip();
                slot.dragAndDropable.dragable = true;
                slot.image.color = Color.white;
                slot.image.sprite = item.image;
                slot.amountOverlay.SetActive(item.amount > 1);
                slot.amountText.text = item.amount.ToString();
            } else {
                // refresh invalid item
                slot.button.onClick.RemoveAllListeners();
                slot.tooltip.enabled = false;
                slot.dragAndDropable.dragable = false;
                slot.image.color = Color.clear;
                slot.image.sprite = null;
                slot.amountOverlay.SetActive(false);
            }
        }

        // gold
        goldText.text = player.gold.ToString();
    }
}
