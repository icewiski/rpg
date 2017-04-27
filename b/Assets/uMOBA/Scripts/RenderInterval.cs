// Some cameras should only re-render every now and then. Useful for minimap,
// which often eats 50% of the FPS otherwise.
using UnityEngine;

public class RenderInterval : MonoBehaviour {
    [SerializeField] float interval = 0.5f;

    void Awake() {
        GetComponent<Camera>().enabled = false;
        InvokeRepeating("Render", 0 , interval);
    }

    void Render() {
        GetComponent<Camera>().Render();
    }
}
