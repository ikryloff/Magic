using System.Collections.Generic;
using UnityEngine;

public class HumanState : BoardUnitState
{
    private Human _human;
    private void Awake()
    {
        _human = GetComponent<Human>();
    }

    public override void Idle()
    {
        _human.SetCurrentSpeed (_human.GetSpeed());
        _animator.AnimateWalk ();
    }

    public override void Stop()
    {
        _human.SetCurrentSpeed (0);
    }

    public override void Die()
    {
        _human.SetCurrentSpeed (0);
        StopAllCoroutines ();
        _animator.StopAllAnimations ();
    }

}
