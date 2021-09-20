using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStateAttack : IUnitState
{
    private BoardUnit _unit;

    public UnitStateAttack( BoardUnit unit )
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

    public void Update()
    {
        throw new System.NotImplementedException ();
    }
}
