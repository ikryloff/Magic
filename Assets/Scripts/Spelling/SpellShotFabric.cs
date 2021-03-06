using System.Collections.Generic;
using UnityEngine;

public class SpellShotFabric : MonoBehaviour
{
    private Cell [] _cells;
    private UnitTemplate _template;
    private int [] _targetLines;
    List<Human> _targetHumans;
    TowerUnit _targetTower;
    private FirePoints _firePoints;

    private void Awake()
    {
        _firePoints = FindObjectOfType<FirePoints> ();
    }


    public void Init( UnitTemplate template, Cell [] cells )
    {
        _cells = new Cell [cells.Length];
        _template = template;
        _cells = cells;

        _targetLines = GetTargetLines (cells);
        _targetHumans = new List<Human> ();
    }

    public bool IsValidSpellCall( UnitTemplate template, Cell [] cells )
    {
        if ( template.cost > Wizard.GetManapoints() )
        {
            GameEvents.current.NewGameMessage ("You have no mana!");
            return false;
        }

        if ( template.spellType == SpellUnit.SpellType.AttackSpell || template.spellType == SpellUnit.SpellType.PressureSpell )
        {
            _targetHumans = GetHumanTargets (template, cells);
            if ( _targetHumans.Count == 0 )
            {
                GameEvents.current.NewGameMessage ("No target!");
                return false;
            }
            if( template.spellType == SpellUnit.SpellType.PressureSpell && _targetHumans [0].IsWeak() )
            {
                GameEvents.current.NewGameMessage ("No target!");
                return false;
            }

        }

        if ( template.spellType == SpellUnit.SpellType.HealingSpell || template.spellType == SpellUnit.SpellType.ReturnManaSpell )
        {
            _targetTower = GetTowerTarget (cells);
            if ( _targetTower == null )
            {
                GameEvents.current.NewGameMessage ("No target for using spell!");
                return false;
            }

            if ( template.spellType == SpellUnit.SpellType.HealingSpell )
            {
                if ( _targetTower.GetUnitTemplate().health == _targetTower.GetUnitHealth().GetCurrentHealth() )
                {
                    GameEvents.current.NewGameMessage ("No need!");
                    return false;
                }
            }
                
        }
        return true;
    }

    public void CreateSpellShot( UnitTemplate template )
    {

        switch ( template.spellType )
        {
            case SpellUnit.SpellType.AttackSpell:
                CreateAttacksOfHumans (template, _targetHumans);
                break;
            case SpellUnit.SpellType.PressureSpell:
                GiveWeaknes (_targetHumans[0], template); // must be only one
                break;
            case SpellUnit.SpellType.HealingSpell:
                HealTower (_targetTower, template);
                break;
            case SpellUnit.SpellType.ReturnManaSpell:
                ReturnTowerForMana (_targetTower, template);
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

        switch ( spellTemplate.attackPower )
        {
            case Unit.UnitAttackPower.RandomInLine:
                {
                    for ( int i = 0; i < cells.Length; i++ )
                    {
                        Human human = UnitsOnBoard.GetRandomHumanToAttack (cells [i]);
                        if ( human != null )
                        {
                            units.Add (human);
                        }
                    }

                }
                break;
            case Unit.UnitAttackPower.NearestInLine:
                {
                    for ( int i = 0; i < cells.Length; i++ )
                    {
                        Human human = UnitsOnBoard.GetNearestHumanToAttack (cells [i]);
                        if ( human != null )
                        {
                            units.Add (human);
                        }
                    }

                }
                break;
            case Unit.UnitAttackPower.AllInLine:
                {
                    for ( int i = 0; i < cells.Length; i++ )
                    {
                        List<Human> lineUnits = new List<Human> ();
                        lineUnits = UnitsOnBoard.GetAllHumansInLineToAttack (cells [i]);
                        if ( lineUnits != null )
                        {
                            for ( int j = 0; j < lineUnits.Count; j++ )
                            {
                                units.Add (lineUnits [j]);
                            }
                        }

                    }
                }
                break;
            case Unit.UnitAttackPower.All:
                units = UnitsOnBoard.GetAllHumansToAttack ();
                break;
        }
        return units;
    }

    // its only for TowerUnit
    private TowerUnit GetTowerTarget( Cell [] cells )
    {
        // must be only one cell
        Debug.Log ("TargetCell " + cells [0].GetLinePosition () + " " + cells [0].GetColumnPosition ());
        if ( cells.Length != 1 )
            return null;
        TowerUnit tower = UnitsOnBoard.GetTowerUnitFromCell (cells [0]);
        if ( tower != null )
            return tower;
        else
            return null;

    }

    private GameObject MakeBullet( UnitTemplate spellTemplate )
    {
        return Instantiate (spellTemplate.bulletPrefab, _firePoints.GetRandomPoint ().position, Quaternion.identity);
    }

    private void CreateAttacksOfHumans( UnitTemplate template, List<Human> targets )
    {
        for ( int i = 0; i < targets.Count; i++ )
        {
            GameObject bulletGO = MakeBullet (template);

            ShotSpell shotSpell = new ShotSpell ();
            shotSpell.Attack (bulletGO, targets [i], template);
        }
    }

    private void ReturnTowerForMana( TowerUnit tower, UnitTemplate spellTemplate )
    {
        ShotSpell shotSpell = new ShotSpell ();
        Instantiate (spellTemplate.unitPrefab, tower.transform.position, Quaternion.identity);
        shotSpell.Return (tower);
    }

    private void HealTower( TowerUnit tower, UnitTemplate spellTemplate )
    {
        ShotSpell shotSpell = new ShotSpell ();
        Instantiate (spellTemplate.unitPrefab, tower.transform.position, Quaternion.identity);
        shotSpell.Heal (tower, spellTemplate);
    }

    private void GiveWeaknes( Human human, UnitTemplate spellTemplate )
    {
        Instantiate (spellTemplate.impactPrefab, human.transform.position, Quaternion.identity);
        GameObject weak = Instantiate (spellTemplate.unitPrefab, human.transform.position, Quaternion.identity);
        ShotSpell shotSpell = new ShotSpell ();
        shotSpell.Hurt (weak, human, spellTemplate);
    }
}
