using System.Collections.Generic;
using UnityEngine;

public class SpellShotFabric : MonoBehaviour
{
    private Cell [] _cells;
    private UnitTemplate _template;
    private int [] _targetLines;
    private UnitsOnBoard _unitsOnBoard;
    List<Human> _targetHumans;
    List<TowerUnit> _targetsTowers;
    private FirePoints _firePoints;

    private void Awake()
    {
        _unitsOnBoard = FindObjectOfType<UnitsOnBoard> ();
        _firePoints = FindObjectOfType<FirePoints> ();
    }


    public void Init( UnitTemplate template, Cell [] cells )
    {
        _cells = new Cell [cells.Length];
        _template = template;
        _cells = cells;

        _targetLines = GetTargetLines (cells);
        _targetHumans = new List<Human> ();
        _targetsTowers = new List<TowerUnit> ();
    }

    public bool IsValidSpellCall( UnitTemplate template, Cell [] cells )
    {
        if ( template.cost > PlayerCharacters.GetPlayerMP () )
        {
            GameEvents.current.NewGameMessage ("You have no mana!");
            GameEvents.current.StopCastingAction ();
            return false;
        }

        if ( template.spellType == SpellUnit.SpellType.AttackSpell || template.spellType == SpellUnit.SpellType.PressureSpell )
        {
            _targetHumans = GetHumanTargets (template, cells);
            if ( _targetHumans.Count == 0 )
            {
                GameEvents.current.NewGameMessage ("No target!");
                GameEvents.current.StopCastingAction ();
                return false;
            }
            print (_targetHumans.Count + " targets in " + cells.Length);
        }

        if ( template.spellType == SpellUnit.SpellType.HealingSpell || template.spellType == SpellUnit.SpellType.ReturnManaSpell )
        {
            _targetsTowers = GetTowerTargets (cells);
            Debug.Log (_targetsTowers.Count);
            if ( _targetsTowers == null || _targetsTowers.Count == 0 )
            {
                GameEvents.current.NewGameMessage ("No target for using spell!");
                GameEvents.current.StopCastingAction ();
                return false;
            }
        }
        return true;
    }

    public void CreateSpellShot(UnitTemplate template)
    {
              
        switch ( template.spellType )
        {
            case SpellUnit.SpellType.AttackSpell:
                CreateAttacks (template, _targetHumans);
                break;
            case SpellUnit.SpellType.PressureSpell:
                break;
            case SpellUnit.SpellType.HealingSpell:
                break;
            case SpellUnit.SpellType.ReturnManaSpell:
                break;


        }
    }

    private int [] GetTargetLines( Cell [] cells )
    {
        int [] targetLines = new int [cells.Length];
        for ( int i = 0; i < cells.Length; i++ )
        {
            targetLines [i] = cells [i].GetLinePosition ();
        }

        return targetLines;
    }

    // its only for Humans
    private List<Human> GetHumanTargets( UnitTemplate spellTemplate, Cell [] cells )
    {
        List<Human> units = new List<Human> ();
        //Random in line
        //for ( int i = 0; i < cells.Length; i++ )
        //{
        //    if ( _unitsOnBoard.GetRandomHumanToAttack (cells [i]) != null )
        //    {
        //        units.Add (_unitsOnBoard.GetRandomHumanToAttack (cells [i]));
        //    }
        //}
        // all in line
        for ( int i = 0; i < cells.Length; i++ )
        {
            List<Human> lineUnits = new List<Human> ();
            lineUnits = _unitsOnBoard.GetAllHumansInLineToAttack (cells [i]);
            if ( lineUnits != null )
            {
                for ( int j = 0; j < lineUnits.Count; j++ )
                {
                    units.Add (lineUnits [j]);
                }
            }
                
        }
        return units;
    }

    // its only for TowerUnit
    private List<TowerUnit> GetTowerTargets( Cell [] cells )
    {
        List<TowerUnit> towers = new List<TowerUnit> ();
        // must be only one cell
        Debug.Log ("TargetCell " + cells[0].GetLinePosition() + " " + cells [0].GetColumnPosition ());
        if ( cells.Length != 1 )
            return null;
        TowerUnit tower = _unitsOnBoard.GetTowerUnitFromCell (cells [0]);
        if ( tower != null )
            towers.Add (tower);
        return towers;
    }

    private GameObject MakeBullet( UnitTemplate spellTemplate )
    {
        return Instantiate (spellTemplate.bulletPrefab, _firePoints.GetRandomPoint ().position, Quaternion.identity);
    }

    private void CreateAttacks( UnitTemplate template, List <Human> targets  )
    {
        for ( int i = 0; i < targets.Count; i++ )
        {
            GameObject bulletGO = MakeBullet (template);

            ShotSpell shotSpell = new ShotSpell ();
            shotSpell.Attack (bulletGO, targets[i], template);
        }
    }
}
