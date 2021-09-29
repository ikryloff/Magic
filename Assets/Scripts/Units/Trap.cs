using System.Collections.Generic;
using UnityEngine;

public class Trap : TowerUnit
{
    public override void Fire( BoardUnit enemy )
    {
        if ( enemy )
        {
            GameEvents.current.NewHit (enemy, _unitTemplate);
            MakeDeath ();
        }
    }

    public override void MakeDeath()
    {
        UnitsOnBoard.RemoveTowerFromLineTowersList (this);
        SetDieState ();
        Instantiate (_death, transform.position, Quaternion.identity);
        Destroy (gameObject);
    }

}
