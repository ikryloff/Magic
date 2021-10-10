using UnityEngine;

public class UnitStateHit : IUnitState
{

    private BoardUnit _unit;
    private UnitTemplate _template;
    private UCUnitAnimation _unitAnimation;


    public UnitStateHit( BoardUnit unit, UnitTemplate template, UCUnitAnimation unitAnimation )
    {
        _unit = unit;
        _template = template;
        _unitAnimation = unitAnimation;
    }


    public void Enter()
    {
        if ( _unit == null )
            return;
        GameEvents.current.OnAnimationFinishedAction += ExitCondition;
        if(_unitAnimation != null )
        {
            if ( _unit.GetDirection () == Constants.UNIT_LEFT_DIR )
                _unitAnimation.AnimateHitWhenLeft ();
            else
                _unitAnimation.AnimateHitWhenRight ();
        }
        else
            _unit.SetHoldState ();

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
