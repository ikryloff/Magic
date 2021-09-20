using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStateHold : IUnitState
{
    private BoardUnit _unit;

    public UnitStateHold( BoardUnit unit )
    {
        _unit = unit;
    }
    public void Enter()
    {
        throw new System.NotImplementedException ();
    }

    public void Exit()
    {
        throw new System.NotImplementedException ();
    }

    
    void IUnitState.Update()
    {
        throw new System.NotImplementedException ();
    }
}
