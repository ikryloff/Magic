using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerState : BoardUnitState
{
    private Tower _tower;
    private void Awake()
    {
        _tower = GetComponent<Tower> ();
    }

    public override void Idle()
    {
        _animator.AnimateStayRight();
    }

    public override void Die()
    {
        StopAllCoroutines ();
        _animator.StopAllAnimations ();
    }
}
