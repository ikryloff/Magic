using System;
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
    public TowerType towerType;
    public int towerLevel;
    public int towerCost;

    

    public SpellUnit.SpellType spellType;

    public ParticleSystem appearParticles;
    
    public virtual void Activate( UnitTemplate template,  Cell cell )
    {
        _unitTemplate = template;
        towerCost = template.cost;
        _name = template.unitName;
        towerType = template.towerType;
        SetLinePosition (cell.GetLinePosition());
        SetColumnPosition (cell.GetColumnPosition());
        DisplaceZPosition ();
    }

    public virtual void FindTarget() { }

    

    public virtual void FreeCell(Cell cell) { }

    public virtual void Die() { }

    public virtual void TakeDamage( float damage ) { }

    public virtual void Attack( float damage, UnitClassProperty classProperty, GameObject bullet ) { }

   

}
