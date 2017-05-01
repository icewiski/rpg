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
	/// Similar to a boolean, except there is an intermediate variable between true and false.
	/// </summary>
	public enum Status
	{
		/// <summary>
		/// Operation failed to complete
		/// </summary>
		Failure,

		/// <summary>
		/// Operation finished
		/// </summary>
		Success,
		
		/// <summary>
		/// Operation in progress
		/// </summary>
		InProgress
	}

	/// <summary>
	/// A static class containing extension methods related to the <see cref="Status"/> enum.
	/// </summary>
	public static class StatusExtensions
	{
		/// <summary>
		/// Converts a boolean value to a <see cref="Status"/>.
		/// </summary>
		/// <param name="variable">The boolean value.</param>
		/// <returns>The equivalent status.</returns>
		public static Status ToStatus(this bool variable)
		{
			return variable ? Status.Success : Status.Failure;
		}
	}
}
