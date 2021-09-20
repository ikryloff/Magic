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
    protected Cell _cell;

    public SpellUnit.SpellType spellType;

    public ParticleSystem appearParticles;

    public void Activate( UnitTemplate template,  Cell cell )
    {
        _cell = cell;
        towerType = template.towerType;
        SetLinePosition (cell.GetLinePosition());
        SetColumnPosition (cell.GetColumnPosition());
        Init (template);
        towerCost = template.cost;

       // _boardUnitState = GetComponent<BoardUnitState> ();
       // _boardUnitState.Init ();
    }

    public override void MakeDeath()
    {
        GameEvents.current.TowerUnitDeathEvent (this, Board.GetCellByPosition(new CellPos(GetColumnPosition(), GetLinePosition())));
        _cell.SetFreefromTower ();
        Instantiate (_death, transform.position, Quaternion.identity);
        Destroy (gameObject);
    }


    public virtual void FindTarget() { }

    

    public virtual void FreeCell(Cell cell) { }

    public virtual void Die() { }

    public virtual void TakeDamage( float damage ) { }

    public virtual void Attack( float damage, UnitClassProperty classProperty, GameObject bullet ) { }

   

}
