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
using UnityEngine.UI;


namespace DAShooter {
	public class VolumeHandler : MonoBehaviour {

		// Use this for initialization
		void Start () 
		{
			if(GameObject.Find("EffectsSlider"))
			GameObject.Find("EffectsSlider").GetComponent<Slider>().onValueChanged.AddListener(SetVolume);
		}

		void SetVolume(float volume)
		{
			GetComponent<AudioSource>().volume = volume;
		}

		void OnDestroy()
		{
			if(GameObject.Find("EffectsSlider"))
			GameObject.Find("EffectsSlider").GetComponent<Slider>().onValueChanged.RemoveListener(SetVolume);
		}
	}
}
