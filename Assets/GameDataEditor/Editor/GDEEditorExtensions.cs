using UnityEngine;
using System.Collections;

namespace GameDataEditor
{
	public static class EditorGUIStyleExtensions
	{
		public static bool IsNullOrEmpty(this GUIStyle variable)
		{
			return variable == null || string.IsNullOrEmpty(variable.name);
		}
	}

	public static class EditorStringExtensions
	{
		static char[] dirSeparators = {'\\', '/'};
		public static string TrimLeadingDirChars(this string variable)
		{
			return variable.TrimStart(dirSeparators);
		}
	}
}