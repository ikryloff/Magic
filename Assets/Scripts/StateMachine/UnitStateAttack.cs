using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStateAttack : IUnitState
{
    private BoardUnit _unit;
    private UnitAnimation _animator;
    private BoardUnit _enemy;

    public UnitStateAttack( BoardUnit unit, UnitAnimation animator )
    {
        _unit = unit;
        _animator = animator;
    }

    public void Enter()
    {
        GameEvents.current.OnAnimationFinishedAction += ExitCondition;
        _enemy = _unit.GetCurrentEnemy ();

        if ( _enemy )
        {
            Debug.Log ("Attack" + _enemy.name + " " + Time.time);
            AttackWithDirection (_enemy);
        }
    }

    public void Exit()
    {
        GameEvents.current.OnAnimationFinishedAction -= ExitCondition;
    }

    public void Tick() { return; }

    private void ExitCondition( BoardUnit unit, string animType )
    {
        if ( _unit == unit )
        {
            if ( animType == Constants.ANIM_UNITY_ATTACK_LEFT || animType == Constants.ANIM_UNITY_ATTACK_RIGHT )
            {
                _unit.Fire (_enemy);
                _unit.SetHoldState ();
            }
        }
    }

    private void AttackWithDirection(BoardUnit enemy)
    {
        if(enemy.transform.position.x >= _unit.transform.position.x )
        {
            _unit.SetDirection (Constants.UNIT_RIGHT_DIR);
            _animator.AttackRightAnimation ();
        }
        else
        {
            _unit.SetDirection (Constants.UNIT_LEFT_DIR);
            _animator.AttackLeftAnimation ();
        }
    }

    
}
