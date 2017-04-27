// This component can be attached to moveable windows, so that they are only
// moveable within the Screen boundaries.
using UnityEngine;

public class UIKeepInScreen : MonoBehaviour {
    void Update () {
        // get current rectangle
        var r = GetComponent<RectTransform>().rect;
            
        // to world space
        Vector2 minworld = transform.TransformPoint(r.min);
        Vector2 maxworld = transform.TransformPoint(r.max);
        var sizeworld = maxworld - minworld;
        
        // keep the min position in screen bounds - size
        maxworld = new Vector2(Screen.width, Screen.height) - sizeworld;
        
        // keep pos between (0,0) and maxworld
        float x = Mathf.Clamp(minworld.x, 0, maxworld.x);
        float y = Mathf.Clamp(minworld.y, 0, maxworld.y);
        
        // now adjust the position
        // (can't just change window.position because that's the pivot)
        // 1. calculate the current offset between r.min and position
        Vector2 offset = (Vector2)transform.position - minworld;
        
        // 2. set the new position + offset
        transform.position = new Vector2(x, y) + offset;
    }
}
