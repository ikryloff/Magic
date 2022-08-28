using System.Collections.Generic;
using UnityEngine;

public class SpellsMaps : MonoBehaviour
{
    public UnitTemplate [] spellScript;

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




    public static Dictionary<string, UnitTemplate> spellsSpellCodeMap;
    public static Dictionary<int, UnitTemplate> spellsIDMap;
    public static Dictionary<SpellProperty, List<int>> spellIDsListByPropertyMap;

    public void Init()
    {
        MakeSpellsStringMap ();
        MakeSpellsIDMap ();
        MakeSpellIDsListByPropertyMap ();

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
                //here for test
                //Player.AddSpellToPlayerSpellsIDList (ss.unitID);
            }
        }
    }

    private void MakeSpellIDsListByPropertyMap()
    {
        spellIDsListByPropertyMap = new Dictionary<SpellProperty, List<int>> ();
        foreach ( var ss in spellScript )
        {
            if ( ss.unitType != Unit.UnitType.Human )
            {
                SpellProperty spellProperty = new SpellProperty (ss.classProperty, ss.unitType);
                if ( !spellIDsListByPropertyMap.ContainsKey (spellProperty) )
                    spellIDsListByPropertyMap [spellProperty] = new List<int> ();
                spellIDsListByPropertyMap [spellProperty].Add (ss.unitID);
                spellIDsListByPropertyMap [spellProperty].Sort ();


               //Debug.Log ("array = " + ss.classProperty + " " + ss.unitType + " " + string.Join (" ",
               //    new List<int> (spellIDsListByPropertyMap [spellProperty])
               //    .ConvertAll (i => i.ToString ())
               //    .ToArray ()));
            }

        }

    }

    public static UnitTemplate GetUnitTemplateBySpellProperty( SpellProperty spellProperty, int num )
    {
        if ( spellIDsListByPropertyMap.ContainsKey (spellProperty) )
        {
            int id = 0;
            if (num < spellIDsListByPropertyMap [spellProperty].Count)
                 id = spellIDsListByPropertyMap [spellProperty] [num];
            return GetUnitTemplateByID (id);
        }
        else return null;
    }

    public static UnitTemplate GetUnitTemplateByID( int id )
    {
        if ( spellsIDMap.ContainsKey (id) )
            return spellsIDMap [id];
        else
            return spellsIDMap [0];

    }
 }

public struct SpellProperty
{
    private Unit.UnitClassProperty school;
    private Unit.UnitType type;

    public SpellProperty( Unit.UnitClassProperty school, Unit.UnitType type )
    {
        this.school = school;
        this.type = type;
    }
}


