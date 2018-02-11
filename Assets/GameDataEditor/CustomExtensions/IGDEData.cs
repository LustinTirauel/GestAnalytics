using UnityEngine;
using System;
using System.Collections.Generic;

namespace GameDataEditor
{
	public abstract partial class IGDEData
	{
		public IGDEData()
		{
			_key = string.Empty;
		}
		
		public IGDEData(string key)
		{
			object temp;
			if (GDEDataManager.DataDictionary.TryGetValue(key, out temp))
				LoadFromDict(key, temp as Dictionary<string, object>);
		}
		
		protected string _key;
		public string Key
		{
			get { return _key; }
			private set { _key = value; }
		}
		
		public abstract void LoadFromDict(string key, Dictionary<string, object> dict);
	}
}
