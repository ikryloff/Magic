using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStateAttack : IUnitState
{
    private BoardUnit _unit;
    private UnitAnimation _unitAnimation;
    private BoardUnit _enemy;
    private TargetFinder _targetFinder;
    private Weapon _weapon;

    public UnitStateAttack( BoardUnit unit, UnitAnimation unitAnimation, TargetFinder targetFinder, Weapon weapon )
    {
        _unit = unit;
        _unitAnimation = unitAnimation;
        _targetFinder = targetFinder;
        _weapon = weapon;
    }

    public void Enter()
    {
        GameEvents.current.OnAnimationFinishedAction += ExitCondition;
        _enemy = _targetFinder.GetRandomTarget ();

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
                _weapon.Fire (_enemy);
                _unit.SetHoldState ();
            }
        }
        else
            _unit.SetHoldState ();
    }

    private void AttackWithDirection(BoardUnit enemy)
    {
        if ( _unitAnimation == null )
        {
            _weapon.Fire (_enemy);
            return;
        }

        if(enemy.transform.position.x >= _unit.transform.position.x )
        {
            _unit.SetDirection (Constants.UNIT_RIGHT_DIR);
            _unitAnimation.AttackRightAnimation ();
        }
        else
        {
            _unit.SetDirection (Constants.UNIT_LEFT_DIR);
            _unitAnimation.AttackLeftAnimation ();
        }
    }

    
}
