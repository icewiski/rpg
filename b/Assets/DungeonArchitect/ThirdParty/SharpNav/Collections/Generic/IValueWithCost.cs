/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// Copyright (c) 2013-2014 Robert Rouhani <robert.rouhani@gmail.com> and other contributors (see CONTRIBUTORS file).
// Licensed under the MIT License - https://raw.github.com/Robmaister/SharpNav/master/LICENSE

namespace SharpNav.Collections.Generic
{
	/// <summary>
	/// An interface that defines a class containing a cost associated with the instance.
	/// Used in <see cref="PriorityQueue{T}"/>
	/// </summary>
	public interface IValueWithCost
	{
		/// <summary>
		/// Gets the cost of this instance.
		/// </summary>
		float Cost { get; }
	}
}
