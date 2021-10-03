using UnityEngine;

public class UnitStateIdle : IUnitState
{
    private BoardUnit _unit;
    private UnitTemplate _template;
    private UnitAnimation _unitAnimation;

    public UnitStateIdle( BoardUnit unit, UnitTemplate template, UnitAnimation unitAnimation )
    {
        _unit = unit;
        _template = template;
        _unitAnimation = unitAnimation;
    }

    public void Enter()
    {
        _unit.SetStartDirection (_template);
        _unitAnimation?.IdleAnimation ();
    }

    public void Exit()
    {
    }

    public void Tick()
    {
        _unit.IdleBehavior ();
    }
}
