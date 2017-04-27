// The Fog of War plane in 3D should be slightly above the ground, so that trees
// etc. aren't darkened, only the ground. Entities will never stand above it,
// because no entities should be visible where there is fog of war anyway.
//
// Note: it finds the local player, which means that it's a client sided effect
//       and we don't have to worry about performance issues on the server.
//
// Note: we use a Unlit/TransparentBlur shader to blur the cutout circles.
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class FogOfWar : MonoBehaviour {
    [SerializeField] Color color = Color.black;
    [SerializeField] int resolution = 256; // affects quality and performance

    // cache
    Texture2D tex;
    Color[] clearArray;

    void Awake() {
        // create texture dynamically
        tex = new Texture2D(resolution, resolution, TextureFormat.RGBA32, false);
        tex.wrapMode = TextureWrapMode.Clamp;
        GetComponent<Renderer>().material.mainTexture = tex;

        // create the clear array once, so we can copy it directly instead of
        // looping through n*n pixels each time
        clearArray = new Color[resolution * resolution];
        for (int i = 0; i < resolution * resolution; ++i)
            clearArray[i] = color;

        // enable renderer
        // (disabled by default for easier scene overview)
        GetComponent<MeshRenderer>().enabled = true;
    }

    // iterate through a rectangle, cut out the ones in the circle
    // note: a midpoint circle fill algorithm might be faster, but it's far more
    //       complicated and the gradient would make it even more complicated.
    // note: we blur the circles with a shader
    void CutoutCircle(int x, int y, int radius) {
        int startX = Mathf.Clamp(x - radius, 0, tex.width);
        int startY = Mathf.Clamp(y - radius, 0, tex.height);
        int endX = Mathf.Clamp(x + radius, 0, tex.width);
        int endY = Mathf.Clamp(y + radius, 0, tex.height);
        var cutout = new Color(0, 0, 0, 0);
        var pos = new Vector2(x, y);

        for (int j = startY; j < endY; ++j)
            for (int i = startX; i < endX; ++i)
                if (Vector2.Distance(new Vector2(i, j), pos) <= radius)
                    tex.SetPixel(i, j, cutout);
    }

    void HandleEntity(Transform entity) {
        // 3d position of where we have to draw the circle
        var pos3D = entity.position - transform.position;

        // get the plane size
        var size = GetComponent<Renderer>().bounds.size;

        // calculate pixels per world unit (assuming that it's a square)
        float ppu = tex.width / size.x;

        // 3d position to texture coordinates between 0..128 etc.
        var center2D = new Vector2(tex.width, tex.height) / 2;
        var pos2D = center2D - new Vector2(pos3D.x, pos3D.z) * ppu;

        // cutout a circle around 'visrange' of player
        float visRange = entity.GetComponent<NetworkProximityChecker>().visRange;
        CutoutCircle(Mathf.RoundToInt(pos2D.x),
                     Mathf.RoundToInt(pos2D.y),
                     Mathf.RoundToInt(visRange * ppu));
    }

    void LateUpdate() {
        // find the local player (if any)
        var go = Utils.ClientLocalPlayer();
        if (go != null) {
            var player = go.GetComponent<Player>();
            if (player != null) { // happens after unet disconnects etc.            
                // reset by copying a cached array. two for loops are too slow.
                tex.SetPixels(clearArray);

                // cutout fog of war above player
                HandleEntity(player.transform);

                // cutout fog of war for all team mates
                foreach (var e in Entity.teams[player.team])
                    HandleEntity(e.transform);

                // apply
                tex.Apply();
            }
        }
    }
}
