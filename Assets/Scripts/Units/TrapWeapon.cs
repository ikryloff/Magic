using UnityEngine;

[RequireComponent (typeof (TowerUnit))]
[RequireComponent (typeof (TargetFinder))]

public class TrapWeapon : Weapon
{
    public override void Fire( BoardUnit enemy )
    {
        if ( enemy )
        {
            GameEvents.current.NewHit (enemy, _unitTemplate);
            _unit.MakeDeath ();
        }
    }
}
