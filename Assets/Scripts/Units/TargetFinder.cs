using System.Collections.Generic;
using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    private float _attackRange;
    private BoardUnit _unit;
    private UnitTemplate _unitTemplate;


    public void Init(BoardUnit unit)
    {
        _unit = unit;
        _unitTemplate = _unit.GetUnitTemplate();
        _attackRange = _unitTemplate.attackRange;

        GameEvents.current.OnHumanPositionWasChanged += SeekEnemies;
        GameEvents.current.OnTowerWasBuiltEvent += SeekEnemies;

    }

    private void OnDestroy()
    {
        GameEvents.current.OnHumanPositionWasChanged -= SeekEnemies;
        GameEvents.current.OnTowerWasBuiltEvent -= SeekEnemies;
    }

    public BoardUnit GetRandomTarget()
    {
        if ( _unitTemplate.unitType == Unit.UnitType.Human )
        {
            return GetRandomTower ();
        }
        else
            return GetRandomHuman ();
    }


    public BoardUnit GetRandomTower()
    {
        List<TowerUnit> towers = UnitsOnBoard.LineTowersList [_unit.GetLinePosition()];
        if ( towers.Count == 0 )
            return null;

        List<TowerUnit> towersInRange = new List<TowerUnit> ();

        for ( int i = 0; i < towers.Count; i++ )
        {
            // Traps are invisible
            if ( towers [i].GetTowerType () == TowerUnit.TowerType.Trap )
                continue;
            if ( Mathf.Abs (towers [i].GetColumnPosition () - _unit.GetColumnPosition()) <= _attackRange )
                towersInRange.Add (towers [i]);
        }

        if ( towersInRange.Count == 0 )
            return null;

        return towersInRange [Random.Range (0, towersInRange.Count)];
    }

    public  BoardUnit GetRandomHuman()
    {
        List<Human> humans = UnitsOnBoard.LineHumansLists [_unit.GetLinePosition ()];

        if ( humans.Count == 0 )
            return null;

        List<Human> humansInRange = new List<Human> ();
        for ( int i = 0; i < humans.Count; i++ )
        {
            if ( Mathf.Abs (humans [i].GetColumnPosition () - _unit.GetColumnPosition ()) <= _attackRange )
                humansInRange.Add (humans [i]);
        }
        if ( humansInRange.Count == 0 )
            return null;

        return humansInRange [Random.Range (0, humansInRange.Count)];
    }

    public void SeekEnemies( BoardUnit unit, Cell cell )
    {
        if ( cell.GetLinePosition () != _unit.GetLinePosition())
            return;
        _unit.SetHoldState();
    }
}
