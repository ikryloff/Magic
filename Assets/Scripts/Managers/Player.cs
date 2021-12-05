using System.Linq;

public static class Player
{
    private static int playerLanguage = 1; // 0 - english, 1 - russian
    private static int playerLevel;
    private static float playerXP = 0;
    private static float playerMPPS = 0.3f;
    private static float playerManaBonus = 1.3f;
    // levels Attack, Defence, Intelligence, Learning, Alteration, Regeneration, FastReading
    private static int [] skillLevels = new int [] { 0, 3, 5, 2, 2, 1, 3 };
    
    private static int skillPoints = 10;

    private static int [] playerSpellsIDList =
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
        0, 0, 0, 0, 0, 0, 0
    };

    public static int [] GetPlayerSpellsIDList()
    {
        return playerSpellsIDList;
    }

    public static bool IsSpellInPlayerSpellsIDList( int id )
    {
        return playerSpellsIDList [id] == 1;
    }

    public static void AddSpellToPlayerSpellsIDList( int id )
    {
        playerSpellsIDList [id] = 1;
    }

    public static int GetPlayerSpellsQuantity()
    {
        return playerSpellsIDList.Sum ();
    }

    public static int GetPlayerSpellsValueByIndex( int id )
    {
        return playerSpellsIDList [id];
    }

    public static int [] GetPlayerSkillLevels()
    {
        return skillLevels;
    }

    public static int GetPlayerSkillPoints()
    {
        return skillPoints;
    }

   
    public static void SetPlayerSkillPoints( int value )
    {
        skillPoints = value;
    }

    public static float GetPlayerXP()
    {
        return playerXP;
    }

    public static float GetPlayerMPPS()
    {
        return playerMPPS;
    }

  


    public static void SetPlayerXP( int xp )
    {
        playerXP = xp;
    }

    public static void SetPlayerMPPS( int mp )
    {
        playerMPPS = mp;
    }

    public static int GetPlayerLevel()
    {
        return playerLevel;
    }

    public static void SetPlayerLevel( int level )
    {
        playerLevel = level;
    }

    public static void SetPlayerLanguage( int lang )
    {
        playerLanguage = lang;
    }

    public static int GetPlayerLanguage()
    {
        return playerLanguage;
    }

    public static float GetPlayerManaBonus()
    {
        return Constants.LEVEL_BONUS_RATIO [skillLevels[2]];
    }

    public static float GetPlayerXPBonus()
    {
        return Constants.LEVEL_BONUS_RATIO [skillLevels [3]];
    }

    public static void SetPlayerManaBonus( int bonus )
    {
        playerManaBonus = bonus;
    }

}
