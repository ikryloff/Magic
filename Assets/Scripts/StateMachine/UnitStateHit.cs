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
        if(_unitAnimation != null )
        {
            if ( _unit.GetDirection () == Constants.UNIT_LEFT_DIR )
                _unitAnimation.AnimateHitWhenLeft (this);
            else
                _unitAnimation.AnimateHitWhenRight (this);
        }
        else
            _unit.SetHoldState ();

    }

    public void UnitAfterHit()
    {
        _unit.SetHoldState ();
    }

    public void Tick() { return;}

    public void Exit() { }
}
