/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// Copyright (c) 2014 Robert Rouhani <robert.rouhani@gmail.com> and other contributors (see CONTRIBUTORS file).
// Licensed under the MIT License - https://raw.github.com/Robmaister/SharpNav/master/LICENSE

namespace SharpNav.Crowds
{
	/// <summary>
	/// This state changes depending on what the crowd agent has to do next
	/// </summary>
	public enum TargetState
	{
		/// <summary>
		/// Not in any state
		/// </summary>
		None,

		/// <summary>
		/// Failed to find a new path
		/// </summary>
		Failed,
		
		/// <summary>
		/// Target destination reached.
		/// </summary>
		Valid,
		
		/// <summary>
		/// Requesting a new path
		/// </summary>
		Requesting,
		
		/// <summary>
		/// Add this agent to the crowd manager's path queue
		/// </summary>
		WaitingForQueue,
		
		/// <summary>
		/// The agent is in the path queue
		/// </summary>
		WaitingForPath,
		
		/// <summary>
		/// Changing its velocity
		/// </summary>
		Velocity
	}
}
