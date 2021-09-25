using UnityEngine;

public class UnitStateHold : IUnitState
{
    private BoardUnit _unit;
    private UnitAnimation _animator;
    BoardUnit _enemy;
    private float _delay;
    private float _attackRate;

    public UnitStateHold( BoardUnit unit, UnitAnimation animator )
    {
        _unit = unit;
        _animator = animator;
    }
    public void Enter()
    {
        if(_enemy == null)
            _enemy = _unit.GetRandomTarget ();

        if ( _enemy == null )
        {
            Debug.Log ("Cant see anybody " + _unit.GetUnitName());
            _unit.SetIdleState ();
        }
        else
        {
            _attackRate = _unit.GetUnitRate ();
            _delay = _unit.GetUnitCurrentDelay ();
            SetAttackPosition (_enemy);
        }
    }


    public void Exit()
    {
        _unit.SetUnitCurrentDelay (_delay);
        _unit.SetCurrentEnemy (_enemy);
    }


    public void Tick()
    {
        if ( _enemy != null )
        {
            if ( _delay <= 0 )
            {
                _delay = _attackRate;
                _unit.SetAttackState ();
            }
            _delay -= Time.deltaTime;
        }
        else
            _unit.SetIdleState ();

    }


    private void SetAttackPosition( BoardUnit enemy )
    {
        string dir = "";

        if ( enemy.transform.position.x >= _unit.transform.position.x )
        {
            dir = Constants.UNIT_RIGHT_DIR;
            _animator.AnimateStayRight ();
        }
        else
        {
            dir = Constants.UNIT_LEFT_DIR;
            _animator.AnimateStayLeft ();
        }

        _unit.SetDirection (dir);
        ChangePositionForHumanAttack (_unit, enemy, dir);
    }

    private void ChangePositionForHumanAttack( BoardUnit unit, BoardUnit enemy, string direction )
    {
        if ( unit.GetUnitType () != Unit.UnitType.Human )
            return;

        int newColPos = 0;
        if ( direction.Equals (Constants.UNIT_RIGHT_DIR) )
        {
            newColPos = enemy.GetColumnPosition () - 1;

        }
        if ( direction.Equals (Constants.UNIT_LEFT_DIR) )
        {
            newColPos = enemy.GetColumnPosition () + 1;
        }

        unit.SetColumnPosition (newColPos);
        CellPos newPos = new CellPos (newColPos, unit.GetLinePosition ());
        Cell newCell = Board.GetCellByPosition (newPos);
        unit.transform.position = new Vector3( newCell.transform.position.x, unit.transform.position.y, unit.transform.position.z);
        unit.SetCurrentCell (newCell);
    }
}
