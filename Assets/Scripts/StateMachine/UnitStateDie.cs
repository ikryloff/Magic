﻿public class UnitStateDie : IUnitState
{
    private BoardUnit _unit;
    private UnitAnimation _animator;

    public UnitStateDie( BoardUnit unit, UnitAnimation animator )
    {
        _unit = unit;
        _animator = animator;
    }

    public void Enter()
    {
        _animator.StopAllAnimations ();
    }

    public void Exit()
    {
        return;
    }

    public void Tick()
    {
        return;
    }
}