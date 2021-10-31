using UnityEngine;

public class UnitStateAttack : IUnitState
{
    private BoardUnit _unit;
    private UCUnitAnimation _unitAnimation;
    private BoardUnit _enemy;
    private UCTargetFinder _targetFinder;
    private UCWeapon _weapon;

    public UnitStateAttack( BoardUnit unit, UCUnitAnimation unitAnimation, UCTargetFinder targetFinder, UCWeapon weapon )
    {
        _unit = unit;
        _unitAnimation = unitAnimation;
        _targetFinder = targetFinder;
        _weapon = weapon;
    }

    public void Enter()
    {
        _enemy = _targetFinder.GetRandomTarget ();

        if ( _enemy )
        {
            Debug.Log (_unit.name + "  Attack  " + _enemy.name + " " + Time.time);
            AttackWithDirection (_enemy);
        }
    }

    public void Exit() { }
    

    public void Tick() { return; }


    public void UnitAttack()
    {
        _weapon.Fire (_enemy);
        _unit.SetHoldState ();
    }

    private void AttackWithDirection( BoardUnit enemy )
    {
        if ( _unitAnimation == null )
        {
            _weapon.Fire (enemy);
            _unit.SetHoldState ();
            return;
        }
        
        if ( enemy.transform.position.x >= _unit.transform.position.x )
        {
            _unit.SetDirection (Constants.UNIT_RIGHT_DIR);
            _unitAnimation.AttackRightAnimation (this);
        }
        else
        {
            _unit.SetDirection (Constants.UNIT_LEFT_DIR);
            _unitAnimation.AttackLeftAnimation (this);
        }
    }


}
