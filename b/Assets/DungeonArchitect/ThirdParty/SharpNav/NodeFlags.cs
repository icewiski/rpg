/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// Copyright (c) 2013 Robert Rouhani <robert.rouhani@gmail.com> and other contributors (see CONTRIBUTORS file).
// Licensed under the MIT License - https://raw.github.com/Robmaister/SharpNav/master/LICENSE

using System;
using System.Collections.Generic;

namespace SharpNav
{
	/// <summary>
	/// Determine which list the node is in.
	/// </summary>
	[Flags]
	public enum NodeFlags
	{
		/// <summary>
		/// Open list contains nodes to examine.
		/// </summary>
		Open = 0x01,

		/// <summary>
		/// Closed list stores path.
		/// </summary>
		Closed = 0x02
	}
}
