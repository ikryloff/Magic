using UnityEngine;

public class UnitStateHold : IUnitState
{
    private BoardUnit _unit;
    private UnitAnimation _unitAnimation;
    private TargetFinder _targetFinder;
    private BoardUnit _enemy;
    private float _delay;
    private float _attackRate;

    public UnitStateHold( BoardUnit unit, UnitAnimation unitAnimation, TargetFinder targetFinder, UnitTemplate unitTemplate)
    {
        _unit = unit;
        _unitAnimation = unitAnimation;
        _targetFinder = targetFinder;
        _attackRate = unitTemplate.attackRate;
        _delay = _attackRate;
    }
    public void Enter()
    {
        if ( _targetFinder == null )
            return;
        _enemy = _targetFinder.GetRandomTarget ();

        if ( _enemy == null )
        {
            Debug.Log ("Cant see anybody " + _unit.GetUnitName ());
            _unit.SetIdleState ();
        }
        else
        {
            SetAttackPosition (_enemy);
        }
    }


    public void Exit()
    {
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
        if ( _unitAnimation == null )
            return;
        string dir = "";

        if ( enemy.transform.position.x >= _unit.transform.position.x )
        {
            dir = Constants.UNIT_RIGHT_DIR;
            _unitAnimation.AnimateStayRight ();
        }
        else
        {
            dir = Constants.UNIT_LEFT_DIR;
            _unitAnimation.AnimateStayLeft ();
        }

        _unit.SetDirection (dir);
        ChangePositionForHumanAttack (_unit, enemy, dir);
    }

    private void ChangePositionForHumanAttack( BoardUnit unit, BoardUnit enemy, string direction )
    {
        if ( unit.GetUnitType () != Unit.UnitType.Human )
            return;

        int newColPos = unit.GetColumnPosition ();
        int enemyColPos = enemy.GetColumnPosition ();

        if ( newColPos == enemyColPos )
        {
            if ( direction.Equals (Constants.UNIT_RIGHT_DIR) )
            {
                newColPos = enemyColPos - 1;

            }
            if ( direction.Equals (Constants.UNIT_LEFT_DIR) )
            {
                newColPos = enemyColPos + 1;
            }
            unit.SetColumnPosition (newColPos);
        }
        CellPos newPos = new CellPos (newColPos, unit.GetLinePosition ());
        Cell newCell = Board.GetCellByPosition (newPos);
        unit.transform.position = new Vector3 (newCell.transform.position.x + Utilities.GetPositionDisplace (), unit.transform.position.y, unit.transform.position.z);
    }
}
