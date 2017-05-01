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
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DAShooter {
	public class PauseManager : MonoBehaviour {
		
		public AudioMixerSnapshot paused;
		public AudioMixerSnapshot unpaused;
		
		Canvas canvas;
		
		void Start()
		{
			canvas = GetComponent<Canvas>();
		}
		
		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				canvas.enabled = !canvas.enabled;
				Pause();
			}
		}
		
		public void Pause()
		{
			Time.timeScale = Time.timeScale == 0 ? 1 : 0;
			Lowpass ();
			
		}
		
		void Lowpass()
		{
			if (Time.timeScale == 0)
			{
				paused.TransitionTo(.01f);
			}
			
			else
				
			{
				unpaused.TransitionTo(.01f);
			}
		}
		
		public void Quit()
		{
			#if UNITY_EDITOR 
			EditorApplication.isPlaying = false;
			#else 
			Application.Quit();
			#endif
		}
	}
}
