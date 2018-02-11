using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameDataEditor;

public enum StatType
{
    HP,
    Mana,
    Damage
};

public class Character
{
	GDECharacter_DemoData data;

    int bonusHP = 0;
    int bonusMana = 0;
    int bonusDamage = 0;

    //
    // Character's public Properties that are defined by the data that
    // gets pulled in from the Game Data Editor.
    //
	public string Name
	{
		get
		{
			return data.name;
		}
	}
    
	public int HP
    {
        get
        {
            return data.hp + bonusHP;
        }
    }

	public int Mana
    {
        get
        {
            return data.mana + bonusMana;
        }
    }

	public int Damage
    {
        get
        {
            return data.damage + bonusDamage;
        }
    }

    public List<GDEBuff_DemoData> Buffs
	{
		get 
		{ 
			return data.buffs;
		}
	}

    /// <summary>
    /// Constructor that takes the data returned by the GDEDataManager.Get() method and will populate
    /// using the generated LoadFromDict method inherited from the GDECharacterData class.
    /// </summary>
    /// <param name="characterData">Character Data Dictionary.</param>
    public Character(string key)
    {
        if (!string.IsNullOrEmpty(key))
        {
            // Load the base character stats using the generated
			// data class
			data = new GDECharacter_DemoData(key);

            // Now iterate over that list of data objects and use them to initialize the Buff class.
            foreach(var buff in Buffs)
            {                  
                //
                // Now add the Buff's 
                // bonuses to the HP, Mana, and Damage. 
                //
                bonusHP += buff.hp_delta;
                bonusMana += buff.mana_delta;
                bonusDamage += buff.damage_delta;
            }
        }
    }

	/// <summary>
    /// Pretty print the Stats (HP, Mana, Damage) and Bonuses in the Scene.
    /// </summary>
    /// <returns>Formated stat string.</returns>
    /// <param name="type">Stat Type.</param>
    public string FormatStat(StatType type)
    {
        string formattedStat;

        switch(type)
        {
            case StatType.HP:
            {
                formattedStat = string.Format("HP: {0}{1}", HP, FormatBonus(type));
                break;
            }

            case StatType.Mana:
            {
                formattedStat = string.Format("Mana: {0}{1}", Mana, FormatBonus(type));
                break;
            }

            case StatType.Damage:
            {
                formattedStat = string.Format("Damage: {0}{1}", Damage, FormatBonus(type));
                break;
            }

            default:
            {
                formattedStat = "Format for "+type.ToString()+" not defined!";
                break;
            }
        }

        return formattedStat;
    }

    string FormatBonus(StatType type)
    {
        string formattedBonus;

        switch(type)
        {
            case StatType.HP:
            {
                if (bonusHP != 0)
                    formattedBonus = string.Format(" ({0}{1})", bonusHP>0?"+":"", bonusHP);
                else
                    formattedBonus = "";
                break;
            }

            case StatType.Mana:
            {
                if (bonusMana != 0)
                    formattedBonus = string.Format(" ({0}{1})", bonusMana>0?"+":"", bonusMana);
                else
                    formattedBonus = "";
                break;
            }

            case StatType.Damage:
            {
                if (bonusDamage != 0)
                    formattedBonus = string.Format(" ({0}{1})", bonusDamage>0?"+":"", bonusDamage);
                else
                    formattedBonus = "";
                break;
            }

            default:
            {
                formattedBonus = "";
                break;
            }
        }

        return formattedBonus;
    }
}
