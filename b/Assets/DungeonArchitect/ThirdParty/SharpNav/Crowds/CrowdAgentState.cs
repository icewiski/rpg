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

using System;

namespace SharpNav.Crowds
{
	/// <summary>
	/// Describes the current state of a crowd agent
	/// </summary>
	[Flags]
	public enum AgentState
	{
		/// <summary>
		/// Not in any state
		/// </summary>
		Invalid,

		/// <summary>
		/// Walking on the navigation mesh
		/// </summary>
		Walking,

		/// <summary>
		/// Handling an offmesh connection
		/// </summary>
		Offmesh
	}
}
