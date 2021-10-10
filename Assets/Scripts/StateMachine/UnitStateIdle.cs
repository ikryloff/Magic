using UnityEngine;

public class UnitStateIdle : IUnitState
{
    private BoardUnit _unit;
    private UnitTemplate _template;
    private UCUnitAnimation _unitAnimation;

    public UnitStateIdle( BoardUnit unit, UnitTemplate template, UCUnitAnimation unitAnimation )
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
