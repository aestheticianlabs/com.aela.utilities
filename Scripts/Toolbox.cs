using System.Collections.Generic;
using JetBrains.Annotations;

namespace AeLa.Utilities
{
	/// <summary>
	/// Holds stuff!
	/// </summary>
	[PublicAPI]
	public static class Toolbox
	{
		private static readonly Dictionary<string, object> objects = new();

		/// <summary>
		/// Adds an object to the toolbox
		/// </summary>
		public static void Add(string key, object value) => objects.Add(key, value);

		/// <summary>
		/// Removes an object from the toolbox
		/// </summary>
		public static void Remove(string key) => objects.Remove(key);

		/// <summary>
		/// Gets an object from the toolbox and attempts to cast it to the provided type
		/// </summary>
		public static T Get<T>(string key) => (T)objects[key];

		/// <summary>
		/// Attempts to get the object of the provided type from the toolbox
		/// </summary>
		/// <returns>Whether or not the object was found</returns>
		public static bool TryGet<T>(string key, out T value)
		{
			try
			{
				value = Get<T>(key);
				return true;
			}
			catch (KeyNotFoundException)
			{
				value = default;
				return false;
			}
		}
	}
}