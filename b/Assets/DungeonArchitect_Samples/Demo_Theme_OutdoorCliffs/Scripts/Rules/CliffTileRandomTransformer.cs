/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System.Collections;
using DungeonArchitect;

public class CliffTileRandomTransformer : TransformationRule {
    public float tileWidth = 3;
    public override void GetTransform(PropSocket socket, DungeonModel model, Matrix4x4 propTransform, System.Random random, out Vector3 outPosition, out Quaternion outRotation, out Vector3 outScale)
    {
        var halfWidth = tileWidth / 2.0f;
        outPosition = new Vector3(
            random.Range(-halfWidth, halfWidth), 0,
            random.Range(-halfWidth, halfWidth));

        outRotation = Quaternion.Euler(0, random.Range(0, 360), 0);
        outScale = Vector3.one;
    }
}
