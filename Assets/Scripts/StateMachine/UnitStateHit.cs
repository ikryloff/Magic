using UnityEngine;

public class UnitStateHit : IUnitState
{

    private BoardUnit _unit;
    private UnitTemplate _template;
    private UnitAnimation _unitAnimation;
    private ParticleSystem _impactParticle;


    public UnitStateHit( BoardUnit unit, UnitTemplate template, UnitAnimation unitAnimation )
    {
        _unit = unit;
        _template = template;
        _unitAnimation = unitAnimation;
        _impactParticle = _unit.GetImpactObject ();
    }


    public void Enter()
    {
        if ( _unit == null )
            return;
        GameEvents.current.OnAnimationFinishedAction += ExitCondition;
        ShowUnitImpact ();
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


    public void ShowUnitImpact()
    {
        _impactParticle?.Play ();
    }

   
}
