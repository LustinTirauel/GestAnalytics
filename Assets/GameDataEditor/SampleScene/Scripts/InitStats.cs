using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameDataEditor;

public class InitStats : MonoBehaviour {

    public GUIText CharacterName;
    public GUIText HitPoints;
    public GUIText Mana;
    public GUIText Damage;
	public TextAsset GDEDataFile;

    public List<GUIText> Buffs;

    Character character = null;

    //
    // Initialize Game Data 
    //
    // Game Data can be initialized in the Start method. 
    // Start is called on the frame when the script is enabled.
    // 
    // Here we will initialize the GDEDataManager and load 
	// the "warrior" item by passing the key to the Character class.
    //
    void Start () 
    {
        // Initialize with the file that was set in the scene
        if (GDEDataFile != null && GDEDataManager.Init(GDEDataFile))
        {
            // Pass the key to the Character constructor to load
            // the warrior Character data. Use the static key class that was
			// generated to avoid typos
            character = new Character(GDEDemoItemKeys.Character_Demo_warrior);

            // Set our GUITexts based on what the character loaded
            CharacterName.text = character.Name;
            HitPoints.text = character.FormatStat(StatType.HP);
            Mana.text = character.FormatStat(StatType.Mana);
            Damage.text = character.FormatStat(StatType.Damage);

            // Set our buff descriptions based on what the character loaded
            for(int index=0;  index<Buffs.Count;  index++)
            {
                if (character.Buffs.IsValidIndex(index))                
                    Buffs[index].text = character.Buffs[index].ToFormattedString();
            }
        }
    }
}
