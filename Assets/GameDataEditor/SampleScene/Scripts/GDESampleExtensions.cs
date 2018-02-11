using UnityEngine;
using System.Collections;

namespace GameDataEditor
{
	public static class GDESampleExtensions
	{
		public static string ToFormattedString(this GDEBuff_DemoData variable)
		{
			string buffString = string.Empty;

			if (variable != null)
			{
				bool needsComma = false;
				buffString = variable.name + ": ";
				
				if (variable.hp_delta != 0)
				{
					buffString += string.Format("{0}{1} HP", variable.hp_delta>0?"+":"", variable.hp_delta);
					needsComma = true;
				}
				
				if (variable.mana_delta != 0)
				{
					buffString += string.Format("{0}{1}{2} Mana", needsComma?",":"", variable.mana_delta>0?"+":"", variable.mana_delta);
					needsComma = true;
				}
				
				if (variable.damage_delta != 0)
				{
					buffString += string.Format("{0}{1}{2} Damage", needsComma?",":"", variable.damage_delta>0?"+":"", variable.damage_delta);
				}
			}
			
			return buffString;
		}
	}
}