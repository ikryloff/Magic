using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private UnitType eType;

    public enum UnitType
    {
        Spell,
        Tower,        
        Human
    }

    public enum UnitClassProperty
    {
        Elemental,
        Nature,
        Demonology,
        Necromancy,
        Deffence,
        Hunter,
        Bravery,        
        Nobility,
        Holiness,
        Immunity
    }

    public enum UnitAttackPower
    {
        None,
        AllInLine,
        NearestInLine,
        RandomInLine,
        All,
        
    }
}
