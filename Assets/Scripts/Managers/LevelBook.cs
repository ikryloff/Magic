

using System;
using UnityEngine;

public static class LevelBook
{

    //levels Attack, Defence, Intelligence, Learning, Alteration, Regeneration, FastReading
    private static int [] levelSkillLevels;
    private static int [] tempSkillLevels;
    private static int levelSkillPoints;
    private static int tempSkillPoints;

    private static float manaPoints;
    private static float manaBonus;
    private static float xpPoints;
    private static float xpBonus;
    


    public static void SetDefaultLevelNotes()
    {
        levelSkillLevels = new int [Player.GetPlayerSkillLevels().Length];
        levelSkillLevels = Player.GetPlayerSkillLevels ();
        tempSkillLevels = new int [levelSkillLevels.Length];
        SetSkillLevelsValues (tempSkillLevels, levelSkillLevels);
        levelSkillPoints = Player.GetPlayerSkillPoints ();
        tempSkillPoints = levelSkillPoints;

        manaBonus = Player.GetPlayerManaBonus ();
        manaPoints = Constants.LEVEL_BONUS_RATIO [levelSkillLevels [2]] * Constants.MANA_POINTS;
        xpBonus = Constants.LEVEL_BONUS_RATIO [levelSkillLevels [3]];
        xpPoints = Player.GetPlayerXP ();


    }

    public static void ResetSkillLevelsAndPoints()
    {
        SetSkillLevelsValues (tempSkillLevels, levelSkillLevels);
        tempSkillPoints = levelSkillPoints;
        GameEvents.current.WizardLevelChangeAction ();
    }

    public static void ApplySkillLevelsAndPoints()
    {
        SetSkillLevelsValues (levelSkillLevels, tempSkillLevels);
        levelSkillPoints = tempSkillPoints;
        GameEvents.current.WizardLevelChangeAction ();
    }

    private static void SetSkillLevelsValues( int [] newValues, int [] tempValues )
    {
        for ( int i = 0; i < newValues.Length; i++ )
        {
            newValues [i] = tempValues [i];
        }
    }

    public static int GetPlayerSkillLevel( int index )
    {
        return tempSkillLevels [index];
    }

    public static void SetPlayerSkillLevel( int index, int value )
    {
        tempSkillLevels [index] = value;
    }

    public static void AddOnePointToPlayerSkillLevel( int index )
    {
        tempSkillLevels [index] += 1;
        SubtruckOnePointFromPlayerSkillPoints ();
    }

    public static void SubtruckOnePointFromPlayerSkillPoints()
    {
        if ( !HasSkillPoints () ) return;
        tempSkillPoints -= 1;
    }
    public static bool HasSkillPoints()
    {
        return tempSkillPoints > 0;
    }

    public static int GetPlayerSkillPoints()
    {
        return tempSkillPoints;
    }

    public static bool WasSkillPointChanged()
    {
        return tempSkillPoints != levelSkillPoints;
    }

    public static int GetPlayerManaPoints()
    {
        manaPoints = Constants.LEVEL_BONUS_RATIO [levelSkillLevels[2]] * Constants.MANA_POINTS;
        return Mathf.RoundToInt (manaPoints);
    }

    public static int GetXPPoints()
    {
        return Mathf.RoundToInt (xpPoints);
    }

    public static void AddXPPoints(float newXPPoints)
    {
        xpPoints += newXPPoints * GetXPBonus();
    }

    public static float GetXPBonus()
    {
        return Constants.LEVEL_BONUS_RATIO [levelSkillLevels [3]];
    }
}
