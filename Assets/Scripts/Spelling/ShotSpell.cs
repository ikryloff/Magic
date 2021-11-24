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
        int returnMP =  Mathf.RoundToInt(target.GetUnitTemplate ().cost * 0.5f);
        Debug.Log ("Return " + target.GetUnitName () + " " + returnMP);
        GameEvents.current.ManaWasteAction (-returnMP);
        //target.SetDieState ();
    }

    public void Hurt( GameObject weak, Human human, UnitTemplate spellTemplate )
    {
        Debug.Log ("Weak " + human.name);
        weak.transform.parent = human.transform;
        human.SetWeak (spellTemplate);
    }

}
