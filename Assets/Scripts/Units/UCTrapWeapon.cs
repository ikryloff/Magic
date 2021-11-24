using UnityEngine;

[RequireComponent (typeof (TowerUnit))]
[RequireComponent (typeof (UCTargetFinder))]

public class UCTrapWeapon : UCWeapon
{
    public override void Fire( BoardUnit unit, BoardUnit enemy )
    {
        if ( unit != _unit ) return;
        if ( enemy == null ) return;
        GameEvents.current.NewHit (enemy, _unitTemplate);
        GameEvents.current.DieAction (_unit);
    }
}
