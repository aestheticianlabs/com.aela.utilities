using System;
using UnityEngine;
// ReSharper disable InconsistentNaming

namespace AeLa.Utilities
{
	/// <summary>
	/// <see cref="UnityEngine.RuntimePlatform"/> as enum flags
	/// </summary>
	[Flags]
	public enum RuntimePlatformFlag
	{
		/// <summary>
		///   <para>In the Unity editor on macOS.</para>
		/// </summary>
		OSXEditor = 1 << 0,

		/// <summary>
		///   <para>In the player on macOS.</para>
		/// </summary>
		OSXPlayer = 1 << 1,

		/// <summary>
		///   <para>In the player on Windows.</para>
		/// </summary>
		WindowsPlayer = 1 << 2,

		/// <summary>
		///   <para>In the Unity editor on Windows.</para>
		/// </summary>
		WindowsEditor = 1 << 3,

		/// <summary>
		///   <para>In the player on the iPhone.</para>
		/// </summary>
		IPhonePlayer = 1 << 4,

		/// <summary>
		///   <para>In the player on Android devices.</para>
		/// </summary>
		Android = 1 << 5,

		/// <summary>
		///   <para>In the player on Linux.</para>
		/// </summary>
		LinuxPlayer = 1 << 6,

		/// <summary>
		///   <para>In the Unity editor on Linux.</para>
		/// </summary>
		LinuxEditor = 1 << 7,

		/// <summary>
		///   <para>In the player on WebGL</para>
		/// </summary>
		WebGLPlayer = 1 << 8,

		/// <summary>
		///   <para>In the player on Windows Store Apps when CPU architecture is X86.</para>
		/// </summary>
		WSAPlayerX86 = 1 << 9,

		/// <summary>
		///   <para>In the player on Windows Store Apps when CPU architecture is X64.</para>
		/// </summary>
		WSAPlayerX64 = 1 << 10,

		/// <summary>
		///   <para>In the player on Windows Store Apps when CPU architecture is ARM.</para>
		/// </summary>
		WSAPlayerARM = 1 << 11,

		/// <summary>
		///   <para>In the player on the Playstation 4.</para>
		/// </summary>
		PS4 = 1 << 12,

		/// <summary>
		///   <para>In the player on Xbox One.</para>
		/// </summary>
		XboxOne = 1 << 13,

		/// <summary>
		///   <para>In the player on the Apple's tvOS.</para>
		/// </summary>
		tvOS = 1 << 14,

		/// <summary>
		///   <para>In the player on Nintendo Switch.</para>
		/// </summary>
		Switch = 1 << 15,
		GameCoreXboxSeries = 1 << 17,
		GameCoreXboxOne = 1 << 18,

		/// <summary>
		///   <para>In the player on the Playstation 5.</para>
		/// </summary>
		PS5 = 1 << 19,
		EmbeddedLinuxArm64 = 1 << 20,
		EmbeddedLinuxArm32 = 1 << 21,
		EmbeddedLinuxX64 = 1 << 22,
		EmbeddedLinuxX86 = 1 << 23,

		/// <summary>
		///   <para>In the server on Linux.</para>
		/// </summary>
		LinuxServer = 1 << 24,

		/// <summary>
		///   <para>In the server on Windows.</para>
		/// </summary>
		WindowsServer = 1 << 25,

		/// <summary>
		///   <para>In the server on macOS.</para>
		/// </summary>
		OSXServer = 1 << 26,
		
				// TODO: not exactly sure what version these actually start appearing in
#if UNITY_2022_1_OR_NEWER
		LinuxHeadlessSimulation = 1 << 16,
		QNXArm32 = 1 << 27,
		QNXArm64 = 1 << 28,
		QNXX64 = 1 << 29,
		QNXX86 = 1 << 30,
#endif
	}

	public static class RuntimePlatformFlagExtensions
	{
		public static bool HasFlagFast(this RuntimePlatformFlag value, RuntimePlatformFlag flag)
		{
			return (value & flag) != 0;
		}

		public static bool HasPlatform(this RuntimePlatformFlag value, RuntimePlatform platform)
		{
			return value.HasFlagFast(platform.AsFlag());
		}

		public static RuntimePlatformFlag AsFlag(this RuntimePlatform value)
		{
			return value switch
			{
				RuntimePlatform.OSXEditor => RuntimePlatformFlag.OSXEditor,
				RuntimePlatform.OSXPlayer => RuntimePlatformFlag.OSXPlayer,
				RuntimePlatform.WindowsPlayer => RuntimePlatformFlag.WindowsPlayer,
				RuntimePlatform.WindowsEditor => RuntimePlatformFlag.WindowsEditor,
				RuntimePlatform.IPhonePlayer => RuntimePlatformFlag.IPhonePlayer,
				RuntimePlatform.Android => RuntimePlatformFlag.Android,
				RuntimePlatform.LinuxPlayer => RuntimePlatformFlag.LinuxPlayer,
				RuntimePlatform.LinuxEditor => RuntimePlatformFlag.LinuxEditor,
				RuntimePlatform.WebGLPlayer => RuntimePlatformFlag.WebGLPlayer,
				RuntimePlatform.PS4 => RuntimePlatformFlag.PS4,
				RuntimePlatform.XboxOne => RuntimePlatformFlag.XboxOne,
				RuntimePlatform.tvOS => RuntimePlatformFlag.tvOS,
				RuntimePlatform.Switch => RuntimePlatformFlag.Switch,
				RuntimePlatform.GameCoreXboxSeries => RuntimePlatformFlag.GameCoreXboxSeries,
				RuntimePlatform.GameCoreXboxOne => RuntimePlatformFlag.GameCoreXboxOne,
				RuntimePlatform.PS5 => RuntimePlatformFlag.PS5,
				RuntimePlatform.EmbeddedLinuxArm64 => RuntimePlatformFlag.EmbeddedLinuxArm64,
				RuntimePlatform.EmbeddedLinuxArm32 => RuntimePlatformFlag.EmbeddedLinuxArm32,
				RuntimePlatform.EmbeddedLinuxX64 => RuntimePlatformFlag.EmbeddedLinuxX64,
				RuntimePlatform.EmbeddedLinuxX86 => RuntimePlatformFlag.EmbeddedLinuxX86,
				RuntimePlatform.LinuxServer => RuntimePlatformFlag.LinuxServer,
				RuntimePlatform.WindowsServer => RuntimePlatformFlag.WindowsServer,
				RuntimePlatform.OSXServer => RuntimePlatformFlag.OSXServer,
				// TODO: not exactly sure what version these actually start appearing in
#if UNITY_2022_1_OR_NEWER
				RuntimePlatform.LinuxHeadlessSimulation => RuntimePlatformFlag.LinuxHeadlessSimulation,
				RuntimePlatform.QNXArm32 => RuntimePlatformFlag.QNXArm32,
				RuntimePlatform.QNXArm64 => RuntimePlatformFlag.QNXArm64,
				RuntimePlatform.QNXX64 => RuntimePlatformFlag.QNXX64,
				RuntimePlatform.QNXX86 => RuntimePlatformFlag.QNXX86,
#endif
				_ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
			};
		}
	}
}