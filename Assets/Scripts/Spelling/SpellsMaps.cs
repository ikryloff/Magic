using System.Collections.Generic;
using UnityEngine;

public class SpellsMaps : MonoBehaviour
{
    public UnitTemplate [] spellScript;
    public Board field;
    public string bulletName;
    public TowerBuilder towerBuilder;
    public AttackSpeller attackManager;
    public Wizard wizard;
    private ObjectsHolder oh;
    private UIManager ui;

    private int [] natureSchoolSpellList = { 7, 8, 9, 10, 11, 12, 13 };
    private int [] natureSchoolCallllList = { 42, 43, 44, 45, 46, 47, 48 };
    private int [] elementalSchoolSpellList = { 0, 1, 2, 3, 4, 5, 6 };
    private int [] elementalSchoolCallList = { 35, 36, 37, 38, 39, 40, 41 };
    private int [] demonologySchoolSpellList = { 14, 15, 16, 17, 18, 19, 20 };
    private int [] demonologySchoolCallList = { 49, 50, 51, 52, 53, 54, 55 };
    private int [] necromancySchoolSpellList = { 21, 22, 23, 24, 25, 26, 27 };
    private int [] necromancySchoolCallList = { 56, 57, 58, 59, 60, 61, 62 };
    private int [] defenciveSchoolSpellList = { 28, 29, 30, 31, 32, 33, 34 };
    private int [] defenciveSchoolCallList = { 63, 64, 65, 66, 67, 68, 69 };




    private Dictionary<string, UnitTemplate> spellsSpellCodeMap;
    private Dictionary<int, UnitTemplate> spellsIDMap;

    private void Awake()
    {
        MakeSpellsStringMap ();
        MakeSpellsIDMap ();
    }

    private void MakeSpellsStringMap()
    {
        spellsSpellCodeMap = new Dictionary<string, UnitTemplate> ();
        foreach ( var item in spellScript )
        {
            spellsSpellCodeMap.Add (item.code, item);
        }
    }

    public UnitTemplate GetSpellByString( string code )
    {
        if ( spellsSpellCodeMap.ContainsKey (code) )
            return spellsSpellCodeMap [code];
        return null;
    }

    private void MakeSpellsIDMap()
    {
        spellsIDMap = new Dictionary<int, UnitTemplate> ();
        foreach ( var ss in spellScript )
        {
            if ( ss.unitType != Unit.UnitType.Human )
            {
                spellsIDMap.Add (ss.unitID, ss);
                PlayerCharacters.AddSpellToPlayerSpellsIDList (ss.unitID);
            }
        }
    }


    private void Start()
    {
        oh = ObjectsHolder.Instance;
        field = oh.field;
        towerBuilder = oh.buildingManager;
        attackManager = oh.attackManager;
        wizard = oh.wizard;
        ui = oh.uIManager;
    }


    //public void FindAndActivateSpell( string spellCode, int top, int bottom, int left, int right, List<Cell> cells )
    //{
    //    if ( Wizard.IsStopCasting )
    //        return;
    //    print (spellCode);
    //    Wizard.IsStopCasting = true;
    //    EntityTemplate spell = GetSpell (spellCode);
    //    if ( spell != null )
    //    {
    //        if ( spell != null )
    //        {
    //            ExecuteTrapSpell (spell, top, left);

    //        }
    //        else if ( spell.isTowerActive )
    //        {
    //            ExecuteTowerSpell (spell, top, bottom, left, right);
    //        }
    //        else
    //        {
    //            ExecuteActiveSpell (spell, top, bottom, left, right);
    //        }
    //    }
    //    else
    //    {
    //        PrintMessage ("Unknown spell!!");
    //        oh.castManager.ClearCast ();
    //        Wizard.IsStopCasting = false;
    //    }
    //}

    


    public UnitTemplate GetSpellByID( int id )
    {
        if ( spellsIDMap.ContainsKey (id) )
            return spellsIDMap [id];
        else
            return spellsIDMap [0];

    }

    private void ExecuteTrapSpell( UnitTemplate spell, int top, int left )
    {
        //towerBuilder.BuildTrap (spell, new int [] { top + spell.targetCell [0], left + spell.targetCell [1] });
    }

    //private void ExecuteTowerSpell( EntityTemplate spell, int top, int bottom, int left, int right )
    //{
    //    if ( spell.code.Equals (Constants.SPELL_CODE_SACRIFICE) )
    //    {
    //        attackManager.ReturnMana (spell, new int [] { top + spell.targetCell [0], left + spell.targetCell [1] });
    //    }
    //    else if ( spell.code.Equals (Constants.SPELL_CODE_ENCOURAGEMENT) )
    //    {
    //        attackManager.HealTower (spell, new int [] { top + spell.targetCell [0], left + spell.targetCell [1] });
    //    }
    //}

    //private void ExecuteActiveSpell( EntityTemplate spell, int top, int bottom, int left, int right )
    //{
    //    int [] arr = spell.CalcTarget (top, bottom, left, right);
    //    if ( spell.isEnemyAffect )
    //    {
    //        if ( spell.code.Equals (Constants.SPELL_CODE_SUPPRESSION) )
    //            attackManager.SlowEnemieMoving (spell, arr);
    //    }
    //    else
    //        attackManager.AttackEnemies (spell, arr);
    //}

    public UnitTemplate GetSpell( string code )
    {
        if ( spellsSpellCodeMap.ContainsKey (code) )
            return spellsSpellCodeMap [code];
        return null;
    }

    public void PrintMessage( string message )
    {
        ui.SetMessage (message);
    }


    public int [] GetNatureListByIndex( int index )
    {
        return index == 0 ? natureSchoolSpellList : natureSchoolCallllList;
    }

    public int [] GetElementalListByIndex( int index )
    {
        return index == 0 ? elementalSchoolSpellList : elementalSchoolCallList;
    }

    public int [] GetDemonologyListByIndex( int index )
    {
        return index == 0 ? demonologySchoolSpellList : demonologySchoolCallList;
    }

    public int [] GetNecromancyListByIndex( int index )
    {
        return index == 0 ? necromancySchoolSpellList : necromancySchoolCallList;
    }

    public int [] GetDefensiveListByIndex( int index )
    {
        return index == 0 ? defenciveSchoolSpellList : defenciveSchoolCallList;
    }

    public int GetSchoolLearnedSpells( int [] spells, int [] calls )
    {
        int count = 0;

        for ( int i = 0; i < spells.Length; i++ )
        {
            count += PlayerCharacters.GetPlayerSpellsValueByIndex (spells [i]);
        }

        for ( int i = 0; i < calls.Length; i++ )
        {
            count += PlayerCharacters.GetPlayerSpellsValueByIndex (calls [i]);
        }

        return count;
    }

}


