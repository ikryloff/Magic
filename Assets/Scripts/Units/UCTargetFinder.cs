using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (UCUnitAnimation))]
public class UCTargetFinder : MonoBehaviour
{
    private float _attackRange;
    private BoardUnit _unit;
    private UnitTemplate _unitTemplate;
    private bool _isInBattle;
    private BoardUnit _enemy;
    private float _delay;
    private float _attackRate;


    public void Init(BoardUnit unit)
    {
        _unit = unit;
        _unitTemplate = _unit.GetUnitTemplate();
        _attackRange = _unitTemplate.attackRange;
        _attackRate = _unitTemplate.attackRate;
        _delay = _attackRate;

        GameEvents.current.OnHumanPositionWasChanged += SeekEnemies;
        GameEvents.current.OnTowerWasBuiltEvent += SeekEnemies;

    }

    private void OnDestroy()
    {
        GameEvents.current.OnHumanPositionWasChanged -= SeekEnemies;
        GameEvents.current.OnTowerWasBuiltEvent -= SeekEnemies;
    }


    private void Update()
    {
        if ( !_isInBattle) return;
        AttackQueue ();
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
        List<TowerUnit> towers = UnitsOnBoard.LineTowersLists [_unit.GetLinePosition()];
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

    public void SeekEnemies( BoardUnit unit )
    {
        if ( unit.GetLinePosition () != _unit.GetLinePosition())
            return;

         _enemy = GetRandomTarget ();

        if ( _enemy == null )
        {
            Debug.Log ("Cant see anybody " + _unit.name);
            if(_isInBattle) GameEvents.current.IdleStateAction (_unit);
            _isInBattle = false;
        }
        else
        {
            _isInBattle = true;
            GameEvents.current.StopToFightAction (_unit, _enemy);
        }
    }

    
    public void AttackQueue()
    {
        if(_enemy == null )
        {
            SeekEnemies (_unit);
            return;
        }

        if ( _delay <= 0 )
        {
            _delay = _attackRate;
            GameEvents.current.AttackStartedAction (_unit, _enemy);
        }
        _delay -= Time.deltaTime;
    }
}
