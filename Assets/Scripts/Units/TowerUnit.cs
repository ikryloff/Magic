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
    public string towerName;
    public TowerType towerType;
    public int towerLevel;
    public int towerDamage;
    public int towerHealth;
    public int towerCost;

    

    public SpellUnit.SpellType spellType;

    public ParticleSystem appearParticles;
    
    public virtual void Activate( UnitTemplate template,  Cell cell )
    {
        Name = template.unitName;
        towerType = template.towerType;
        SetLinePosition (cell.GetLinePosition());
        SetColumnPosition (cell.GetColumnPosition());
    }

    public virtual void FindTarget() { }

    

    public virtual void FreeCell(Cell cell) { }

    public virtual void Die() { }

    public virtual void TakeDamage( float damage ) { }

    public virtual void Attack( float damage, UnitClassProperty classProperty, GameObject bullet ) { }

   

}
