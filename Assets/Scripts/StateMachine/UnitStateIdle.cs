using UnityEngine;

public class UnitStateIdle : IUnitState
{
    private BoardUnit _unit;
    private UnitTemplate _template;
    private UnitAnimation _animator;

    public UnitStateIdle( BoardUnit unit, UnitTemplate template, UnitAnimation animator )
    {
        _unit = unit;
        _template = template;
        _animator = animator;
    }

    public void Enter()
    {
        _unit.SetStartDirection (_template);
        _animator.IdleAnimation ();
    }

    public void Exit()
    {
    }

    public void Tick()
    {
        _unit.IdleBehavior ();
    }
}
