using System.Linq;

public static class Player
{
    private static int playerLanguage = 1; // 0 - english, 1 - russian
    private static int playerLevel;
    private static int playerStartMP = 300;
    private static int playerMP = 300;
    private static int playerXP = 0;
    private static float playerMPPS = 0.3f;
    private static int playerManaBonus = 1;
    // levels Attack, Defence, Intelligence, Learning, Alteration, Regeneration, FastReading
    private static int [] skillLevels = new int [] { 0, 0, 0, 0, 0, 0, 0 };
   

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

    public static bool IsSpellInPlayerSpellsIDList( int id)
    {
        return playerSpellsIDList [id] == 1;
    }

    public static void AddSpellToPlayerSpellsIDList (int id)
    {        
        playerSpellsIDList [id] = 1;
    }

    public static int GetPlayerSpellsQuantity()
    {     
        return playerSpellsIDList.Sum ();
    }

    public static int GetPlayerSpellsValueByIndex( int id)
    {
        return playerSpellsIDList[id];
    }

    public static int GetStartPlayerMP()
    {
        return playerStartMP;
    }
    public static int GetPlayerMP()
    {
        return playerMP;
    }

    public static void ChangePlayerMP(int delta)
    {
        playerMP += delta;
    }

    public static int GetPlayerSkillLevel(int index)
    {
        return skillLevels[index];
    }

    public static void SetPlayerSkillLevel( int index, int value)
    {
        skillLevels [index] = value;
    }

    

    public static int GetPlayerXP()
    {
        return playerXP;
    }

    public static float GetPlayerMPPS()
    {
        return playerMPPS;
    }

    public static void SetPlayerMP(int mp)
    {
        playerMP = mp;
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

    public static int GetPlayerLanguage( )
    {
        return playerLanguage;
    }

    public static int GetPlayerManaBonus()
    {
        return playerManaBonus;
    }

    public static void SetPlayerManaBonus( int bonus )
    {
        playerManaBonus = bonus;
    }

}
