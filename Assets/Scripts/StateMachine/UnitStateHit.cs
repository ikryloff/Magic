using UnityEngine;

public class UnitStateHit : IUnitState
{

    private BoardUnit _unit;
    private UnitTemplate _template;
    private UnitAnimation _animator;

    public UnitStateHit( BoardUnit unit, UnitTemplate template, UnitAnimation animator )
    {
        _unit = unit;
        _template = template;
        _animator = animator;
    }


    public void Enter()
    {
        if ( _unit == null )
            return;

        GameEvents.current.OnAnimationFinishedAction += ExitCondition;
        if ( _unit.GetDirection () == Constants.UNIT_LEFT_DIR )
            _animator.AnimateHitWhenLeft ();
        else
            _animator.AnimateHitWhenRight ();
    }

    public void Exit()
    {
        GameEvents.current.OnAnimationFinishedAction -= ExitCondition;
    }

    public void Tick() { return;}

    private void ExitCondition( BoardUnit unit, string animType )
    {
        if ( _unit == unit )
        {
            if ( animType == Constants.ANIM_UNIT_HIT_LEFT || animType == Constants.ANIM_UNIT_HIT_RIGHT )
            {
                _unit.SetHoldState ();
            }
        }
    }
}
