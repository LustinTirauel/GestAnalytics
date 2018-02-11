using UnityEngine;
using UnityEditor;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace GameDataEditor
{
	[Serializable]
	public class GDESettings
	{
		const string ACCESS_TOKEN_KEY = "gde_at";
		const string REFRESH_TOKEN_KEY = "gde_rt";
		const string ACCESS_TOKEN_TIMEOUT_KEY = "gde_t";
		const string CreateDataColorKey = "gde_createdatacolor";
		const string DefineDataColorKey = "gde_definedatacolor";
		const string HighlightColorKey = "gde_highlightcolor";
		const string DataFileKey = "gde_datafile";
		const string WorkbookFilePathKey = "gde_workbookpath";

		public string AccessTokenTimeout;
		public string AccessTokenKey;
		public string RefreshTokenKey;

		bool _prettyJson;
		public bool PrettyJson
		{
			get { return _prettyJson; }
			set
			{
				_prettyJson = value;
			}
		}

		string _dataFilePath;
		public string DataFilePath
		{
			get
			{
				if (string.IsNullOrEmpty(_dataFilePath))
					_dataFilePath = FullRootDir + "/" + GDEConstants.DefaultDataFilePath + "/" + GDEConstants.DataFile;

				return _dataFilePath;
			}
			set
			{
				_dataFilePath = value;
			}
		}

		static string _fullRootDir;
		public static string FullRootDir
		{
			get
			{
				if (!Directory.Exists(_fullRootDir))
				{
					var results = AssetDatabase.FindAssets(GDEConstants.RootDir);
					if (results != null && results.Length > 0)
					{
						string assetPath = AssetDatabase.GUIDToAssetPath(results[0]);
						string currentDir = Environment.CurrentDirectory;

						_fullRootDir = currentDir + Path.DirectorySeparatorChar + assetPath;
					}
					else
						_fullRootDir = Application.dataPath + GDEConstants.RootDir;
				}

				return _fullRootDir;
			}
		}

		public static string RelativeRootDir
		{
			get
			{
				return FullRootDir.Replace(Environment.CurrentDirectory, string.Empty).TrimLeadingDirChars();
			}
		}

		public static string DefaultDataFilePath
		{
			get
			{
				return FullRootDir + "/" + GDEConstants.DefaultDataFilePath + "/" + GDEConstants.DataFile;
			}
		}

		public string CreateDataColor;
		public string DefineDataColor;
		public string HighlightColor;

		static string settingsPath = Path.Combine(FullRootDir, GDEConstants.SettingsPath);

		static GDESettings _instance;
		public static GDESettings Instance
		{
			get
			{
				if (_instance == null)
					Load();
				return _instance;
			}
		}

		GDESettings()
		{
			_dataFilePath = EditorPrefs.GetString(DataFileKey, DefaultDataFilePath);

			if (EditorGUIUtility.isProSkin)
			{
				CreateDataColor = EditorPrefs.GetString(CreateDataColorKey, GDEConstants.CreateDataColorPro);
				DefineDataColor = EditorPrefs.GetString(DefineDataColorKey, GDEConstants.DefineDataColorPro);
			}
			else
			{
				CreateDataColor = EditorPrefs.GetString(CreateDataColorKey, GDEConstants.CreateDataColor);
				DefineDataColor = EditorPrefs.GetString(DefineDataColor, GDEConstants.DefineDataColor);
			}

			HighlightColor = EditorPrefs.GetString(HighlightColorKey, GDEConstants.HighlightColor);
            
			// Delete the editor prefs keys if they exist
			if (EditorPrefs.HasKey(DataFileKey))
				EditorPrefs.DeleteKey(DataFileKey);

			if (EditorPrefs.HasKey(CreateDataColorKey))
				EditorPrefs.DeleteKey(CreateDataColorKey);

			if (EditorPrefs.HasKey(DefineDataColorKey))
				EditorPrefs.DeleteKey(DefineDataColorKey);

			if (EditorPrefs.HasKey(HighlightColorKey))
				EditorPrefs.DeleteKey(HighlightColorKey);
		}

		public void Save()
		{
			using (var stream = new MemoryStream())
			{
				BinaryFormatter bin = new BinaryFormatter();
				bin.Serialize(stream, this);

				File.WriteAllBytes(settingsPath, stream.ToArray());
			}
		}

		static void Load()
		{
			if (File.Exists(settingsPath))
			{
				byte[] bytes = File.ReadAllBytes(settingsPath);

				using (var stream = new MemoryStream(bytes))
				{
					BinaryFormatter bin = new BinaryFormatter();
					_instance = bin.Deserialize(stream) as GDESettings;
				}
			}
			else
			{
				_instance = new GDESettings();
			}
		}
	}
}
