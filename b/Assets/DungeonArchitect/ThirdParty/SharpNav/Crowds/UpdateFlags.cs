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
	/// The UpdateFlag affects the way the agent moves acorss its path.
	/// </summary>
	[Flags]
	public enum UpdateFlags
	{
		/// <summary>
		/// The agent will be making turns in its path
		/// </summary>
		AnticipateTurns = 1,

		/// <summary>
		/// Avoid obstacles on the path
		/// </summary>
		ObstacleAvoidance = 2,

		/// <summary>
		/// Separate this agent from other agents
		/// </summary>
		Separation = 4,

		/// <summary>
		/// Optimize if the agent can see the next corner
		/// </summary>
		OptimizeVis = 8,

		/// <summary>
		/// Optimize the agent's path corridor
		/// </summary>
		OptimizeTopo = 16
	}
}
