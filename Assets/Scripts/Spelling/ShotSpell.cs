using UnityEngine;

public class ShotSpell
{

    public void Attack( GameObject bulletGO, Human target, UnitTemplate template )
    {
        if ( target != null )
            Debug.Log ("Attack " + target.name);

        Bullet bullet = bulletGO.GetComponent<Bullet> ();
        if ( bullet != null && target != null )
        {
            bullet.SeekTarget (target, template);
        }

    }

    public void Heal( TowerUnit target, UnitTemplate template )
    {
        Debug.Log ("Heal " + target.name);
        GameEvents.current.NewHit (target, template );
    }

    public void Return( TowerUnit target )
    {
        Debug.Log ("Return " + target.GetUnitName ());
        target.MakeDeath ();
    }


}
