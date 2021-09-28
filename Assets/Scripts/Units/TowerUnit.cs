using System.Collections.Generic;
using UnityEngine;

public class TowerUnit : BoardUnit
{
    public enum TowerType
    {
        None,
        Trap,
        Tower,
        Barrier,
    }

    public int towerID;
    protected TowerType _towerType;
    public int towerLevel;
    public int towerCost;

    public SpellUnit.SpellType spellType;

    public ParticleSystem appearParticles;

    public virtual void Activate( UnitTemplate template, Cell cell )
    {
        _cell = cell;
        _towerType = template.towerType;
        SetLinePosition (cell.GetLinePosition ());
        SetColumnPosition (cell.GetColumnPosition ());
        Init (template);
        towerCost = template.cost;

    }

    public override void MakeDeath()
    {
        UnitsOnBoard.RemoveTowerFromLineTowersList (this, _cell);
        Instantiate (_death, transform.position, Quaternion.identity);
        SetDieState ();
        Destroy (gameObject);
    }

    public override BoardUnit GetRandomTarget()
    {
        List<Human> humans = UnitsOnBoard.LineHumansLists [_linePosition];

        if ( humans.Count == 0 )
            return null;

        List<Human> humansInRange = new List<Human> ();
        for ( int i = 0; i < humans.Count; i++ )
        {
            if ( Mathf.Abs (humans [i].GetColumnPosition () - _columnPosition) <= _attackRange )
                humansInRange.Add (humans [i]);
        }
        if ( humansInRange.Count == 0 )
            return null;

        return humansInRange [Random.Range (0, humansInRange.Count)];
    }

    public TowerType GetTowerType()
    {
        return _towerType;
    }

}
