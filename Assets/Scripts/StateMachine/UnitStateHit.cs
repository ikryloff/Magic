using System.Collections;
using System.Collections.Generic;
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
        
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        
    }
}
