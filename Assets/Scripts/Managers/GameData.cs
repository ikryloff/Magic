using System;

[Serializable]
public class GameData
{
    public int playerLanguage; // 0 - english, 1 - russian

    public int stage;

    public float xpPoints;
    public int skillPoints;
    public int [] skillLevels;
    public int [] spellsIDList;

    public GameData(Player player, int currentStage)
    {
        stage = currentStage;
        xpPoints = player.GetXPPoints ();
        playerLanguage = player.GetPlayerLanguage ();
        skillPoints = player.GetSkillPoints ();
        skillLevels = player.GetSkillLevels ();
        spellsIDList = player.GetSpellsIDList ();
    }
}
