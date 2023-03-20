using System;
using System.Collections.Generic;
using UnityEngine;

namespace AeLa.Utilities
{
	public static class GUIDHelper
	{
		private static HashSet<string> guids = new();

		public static string GetGUID()
		{
			string guid;
			do
			{
				guid = Guid.NewGuid().ToString();
			}
			while (guids.Contains(guid));
			return guid;
		}

		/// <summary>
		/// Returns whether or not a GUID has already been validated (in use somewhere else).
		/// Only call once per GUID. Will fail validation on subsequent calls.
		/// </summary>
		public static bool ValidateGUID(string guid)
		{
			if (guids.Contains(guid))
			{
				Debug.LogError($"Duplicate GUID {guid} detected.");
				return false;
			}

			guids.Add(guid);
			return true;
		}
	}
}