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
        _cell.SetFreefromTower ();
        Instantiate (_death, transform.position, Quaternion.identity);
        SetDieState ();
        Destroy (gameObject);
    }
}
