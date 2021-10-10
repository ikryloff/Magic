using UnityEngine;

[RequireComponent (typeof (TowerUnit))]
[RequireComponent (typeof (UCTargetFinder))]

public class UCTrapWeapon : UCWeapon
{
    public override void Fire( BoardUnit enemy )
    {
        if ( enemy )
        {
            GameEvents.current.NewHit (enemy, _unitTemplate);
            _unit.SetDieState ();
            
        }
    }
}
