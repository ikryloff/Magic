using System.Collections.Generic;
using UnityEngine;

public class Tower : TowerUnit
{
    private List<Human> _humans;

    private void Awake()
    {
        _humans = new List<Human> ();
    }

    private void OnDisable()
    {
    }

    
    public override void GetEnemies( BoardUnit tower, Cell cell )
    {
        if ( tower.GetType () == typeof (Tower) && tower != this )
            return;

        if ( cell.GetLinePosition () != _linePosition )
            return;

        List<Human> enemies = UnitsOnBoard.LineHumansLists [_linePosition];
        if ( enemies.Count == 0 )
        {
            //_boardUnitState.SetIdleState ();
            return;
        }

        for ( int i = 0; i < enemies.Count; i++ )
        {
            if ( Mathf.Abs (enemies [i].GetColumnPosition () - _columnPosition) <= _attackRange )
            {
                if ( !_humans.Contains (enemies [i]) )
                {
                    _humans.Add (enemies [i]);
                    Debug.Log (name + " Found " + enemies [i].name + " Count " + _humans.Count + " at line " + _linePosition);
                }
            }
        }

        if ( _humans.Count > 0 )
        {
            _boardUnitState.Decide ();
        }
        else
        {
            _boardUnitState.SetIdleState ();
        }
    }



    public override BoardUnit GetRandomTarget()
    {
        if ( _humans.Count > 0 )
            return _humans [Random.Range (0, _humans.Count)];
        else
            return null;
    }


}