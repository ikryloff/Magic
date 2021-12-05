public class Constants
{

    //Camera
    public static float STANDART_SCREEN_HEIGHT = 12.5f;
    // Tags
    public static string CELL_TAG = "Cell";
    public static string HUMAN_TAG = "Human";
    // Sorting layer
    public static string SPELL_SL = "Spell";
    //Shaders
    public static string GRAYSCALE_RATIO = "_GrayscaleAmount";

    //Player
    public static float MANA_POINTS = 100;
    public static int PLAYER_SKILLS_MAX_LEVEL = 5;
    public static float [] LEVEL_BONUS_RATIO = new float [] { 1, 1.1f, 1.2f, 1.5f, 2f, 4f };

    // Board
    public static int BOARD_WIDTH = 16;
    public static int BOARD_HEIGHT = 9;
    public static float TRAP_DIST = 0.3f;
    public static float PATH_LENGHT = 17f;
    public static float PATH_START_X = 10.4f;
    public static float BULLET_SPEED = 40f;
    public static float DEG_180 = 180f;


    public static float TOWER_HEAL_POINTS = 30f;
    
    
    //Strings for animation
    public static string ANIM_BOOL_IS_IN_BATTLE = "isInBattle";
    public static string ANIM_UNIT_HIT= "unit_hit";
    public static string ANIM_UNIT_ATTACK = "unit_attack";
    public static string ANIM_UNIT_STAY = "unit_stay";
    public static string ANIM_UNIT_WALK = "unit_walk";

   
    public static string BULLET_BULLET = "bullet";
    public static string BULLET_ARROW = "arrow";
    public static string BULLET_AE_BULLET = "ae-bullet";
    public static string BLOOD_IMPACT = "bloodImpact";
    public static string CREEP_DEATH = "creepDeath";
    public static string SUPPRESSION_WIND = "suppressionWind";
    public static string DEFFENCE_AFFECT = "defenceAffect";

    // spell targets
    public static string SPELL_TARGET_RANDOM_IN_LINE = "RIL";
    public static string SPELL_TARGET_NEAREST_IN_LINE = "NIL";
    public static string SPELL_TARGET_ALL = "ALL";

   

    //Schools
    public static string NATURE = "NATURE MAGIC";
    public static string ELEMENTAL = "ELEMENTAL MAGIC";
    public static string DEFENSIVE = "DEFENSIVE MAGIC";
    public static string NECROMANCY = "NECROMANCY";
    public static string DEMONOLOGY = "DEMONOLOGY";
    public static string INFO = "INFORMATION";

}
