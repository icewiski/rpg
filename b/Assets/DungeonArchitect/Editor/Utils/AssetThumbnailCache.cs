/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

//$ Copyright 2016, Code Respawn Technologies Pvt Ltd - All Rights Reserved $//

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace DungeonArchitect.Editors
{
    /// <summary>
    /// Manages the asset thumbnails to display in the visual nodes
    /// </summary>
    public class AssetThumbnailCache
    {
        Dictionary<Object, Texture2D> thumbnails = new Dictionary<Object, Texture2D>();

		/// <summary>
		/// List of paths which we have alread requested for reimporting. 
		/// This is used to disallow repeated reimport requests if a thumbnail is still not found
		/// </summary>
		HashSet<string> reimportRequestPaths = new HashSet<string>();

        /// <summary>
        /// The texture to display if the thumbnail for an object cannot be created / retrieved
        /// </summary>
        Texture2D defaultTexture = null;

        private static AssetThumbnailCache instance = null;
        /// <summary>
        /// Singleton access
        /// </summary>
        public static AssetThumbnailCache Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AssetThumbnailCache();
                }
                return instance;
            }
        }

        private AssetThumbnailCache()
        {
            Reset();
            AssetPreview.SetPreviewTextureCacheSize(100);
        }

        /// <summary>
        /// Clears all the thumbnail from the cache
        /// </summary>
        public void Reset()
        {
            thumbnails.Clear();
			reimportRequestPaths.Clear();
            defaultTexture = AssetPreview.GetMiniTypeThumbnail(typeof(GameObject));
        }

        /// <summary>
        /// Gets the thumbnail of the specified asset.   Tries to retrieve it from the cache, if it was accessed earlier
        /// </summary>
        /// <param name="asset">The asset to get the thumbnail for</param>
        /// <returns>The thumbnail of the asset.  If thumbnail cannot be created, returns the defaultTexture instead</returns>
        public Texture2D GetThumb(Object asset)
        {
            if (thumbnails.ContainsKey(asset))
            {
                var thumbnail = thumbnails[asset];
                if (thumbnail != null)
                {
                    return thumbnail;
                }
                else
                {
                    thumbnails.Remove(asset);
                }
            }

            var thumb = AssetPreview.GetAssetPreview(asset);
            if (thumb == null)
            {
				// Try to reimport the asset to generate the thumbnail
				var path = AssetDatabase.GetAssetPath(asset);
				if (path != null && !reimportRequestPaths.Contains(path)) {
					AssetDatabase.ImportAsset(path);
					reimportRequestPaths.Add (path);
				}
				thumb = AssetPreview.GetAssetPreview(asset);
				if (thumb == null) {
					// Still no thumbnail.  Use a default thumbnail texture
					return defaultTexture;
				}
            }

            thumbnails.Add(asset, thumb);
            return thumb;
        }

        // Update is called once per frame
        public void Update()
        {

        }
    }
}

