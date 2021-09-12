using System.Collections.Generic;
using UnityEngine;

public class ShotSpell
{

    public void Attack( GameObject bulletGO, BoardUnit target, UnitTemplate template )
    {
        if(target != null)
            Debug.Log ("Attack " + target.name);

        Bullet bullet = bulletGO.GetComponent<Bullet> ();
        if ( bullet != null && target != null )
        {
            bullet.SeekHuman (target, template);
        }

    }

    public void Heal( Tower target, UnitTemplate template )
    {
        Debug.Log ("Heal " + target.name);

       

    }

    public void Return( TowerUnit target)
    {
        Debug.Log ("Return " + target.GetUnitName());

    }


}
