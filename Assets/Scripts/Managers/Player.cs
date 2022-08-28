using System;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int playerLanguage = 1; // 0 - english, 1 - russian

    private int playerStage;

    private float playerXP = 0;
    // levels Attack, Defence, Intelligence, Learning, Alteration, Regeneration, FastReading
    private int [] skillLevels = new int [] { 0, 0, 0, 0, 0, 0, 0 };

    private int skillPoints = 35;

    private int [] spellsIDList = new int []
    {
        0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0,
    };

    private float _manaPoints;

    public void LoadGameData( GameData data )
    {
        playerLanguage = data.playerLanguage;
        playerStage = data.stage;
        Debug.Log (playerStage);
        playerXP = data.xpPoints;
        skillLevels = new int [data.skillLevels.Length];
        Array.Copy (data.skillLevels, skillLevels, data.skillLevels.Length);
        skillPoints = data.skillPoints;
        spellsIDList = new int [data.spellsIDList.Length];
        Array.Copy (data.spellsIDList, spellsIDList, data.spellsIDList.Length);

        _manaPoints = Constants.LEVEL_BONUS_RATIO [skillLevels [Constants.SL_INT]] * Constants.MANA_POINTS;
    }



    public float GetManaPoints()
    {
        return _manaPoints;
    }

    public void SetManaPoints( float manaPoints )
    {
        _manaPoints = manaPoints;
    }


    public int [] GetSpellsIDList()
    {
        int [] newArray = new int [spellsIDList.Length];
        Array.Copy (spellsIDList, newArray, spellsIDList.Length);
        return newArray;
    }

    public void SetSpellsIDList( int [] sourceArray )
    {
        Array.Copy (sourceArray, spellsIDList, sourceArray.Length);
    }
    public int [] GetSkillLevels()
    {
        int [] newArray = new int [skillLevels.Length];
        Array.Copy (skillLevels, newArray, skillLevels.Length);
        return newArray;
    }

    public void SetSkillLevels( int [] sourceArray )
    {
        Array.Copy (sourceArray, skillLevels, sourceArray.Length);
    }

    public bool IsSpellInPlayerSpellsIDList( int id )
    {
        return spellsIDList [id] == 1;
    }

    public void AddSpellToPlayerSpellsIDList( int id )
    {
        spellsIDList [id] = 1;
    }

    public int GetPlayerSpellsQuantity()
    {
        return spellsIDList.Sum ();
    }

    public int GetPlayerSpellsValueByIndex( int id )
    {
        return spellsIDList [id];
    }


    public int GetSkillPoints()
    {
        return skillPoints;
    }

    public void SetSkillPoints( int value )
    {
        skillPoints = value;
    }

    public float GetXPPoints()
    {
        return playerXP;
    }

    public void AddXPPoints( float points )
    {
        playerXP += points;
    }


    public void SetPlayerXP( int xp )
    {
        playerXP = xp;
    }

    public void SetPlayerStage( int level )
    {
        playerStage = level;
    }

    public void SetPlayerLanguage( int lang )
    {
        playerLanguage = lang;
    }

    public int GetPlayerLanguage()
    {
        return playerLanguage;
    }

    public int GetPlayerStage()
    {
        return playerStage;
    }

    public float GetPlayerManaBonus()
    {
        return Constants.LEVEL_BONUS_RATIO [skillLevels [Constants.SL_INT]];
    }

    public float GetPlayerXPBonus()
    {
        return Constants.LEVEL_BONUS_RATIO [skillLevels [Constants.SL_LRN]];
    }

}
